using HospitalSystem.Models;
using HospitalSystem.ModelVM.Account;
using HospitalSystem.ModelVM.Patient;
using HospitalSystem.Service.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Security.Claims;

namespace HospitalSystem.Controllers
{
    public class AccountController : Controller
    {

            private SignInManager<Patient> _signInManager;
            private UserManager<Patient> _userManager;
            private readonly IAccountService patientService;
            private IConfiguration _configuration;
            public AccountController(SignInManager<Patient> signInManager, UserManager<Patient> userManager, IConfiguration configuration, IAccountService patientService)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _configuration = configuration;
                this.patientService = patientService;
            }
        
            public async Task<IActionResult> Profile()
            {
                var patientProfileVM = await patientService.GetProfile(User);
                if (patientProfileVM == null)
                {
                    return NotFound();
                }

                return View(patientProfileVM);
            }


            public async Task<IActionResult> UpdateProfile()
            {
                var model = await patientService.GetPatientForEdit(User);
                if (model == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> UpdateProfile(EditePatientVM model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await patientService.UpdatePatient(User, model);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                return RedirectToAction("Profile", "Account");
            }


            [HttpGet]
            public async Task<IActionResult> Delete()
            {
                var user = await patientService.GetCurrentPatient(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteAccount()
            {
                var result = await patientService.DeletePatientAccount(User);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Your account has been deactivated.";
                    return RedirectToAction("Register", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("DeleteAccount");
            }

            public IActionResult ChangePassword() => View();

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await patientService.ChangePassword(model);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                TempData["SuccessMessage"] = "Password changed successfully.";
                return RedirectToAction("Profile", "Account");
            }
            //login
            public async Task<IActionResult> Login()
            {
                var loginVM = await patientService.GetLoginViewModelAsync();
                return View(loginVM);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> LoginSubmitted(LoginVM loginVM)
            {
                if (!ModelState.IsValid)
                {
                    return View("Login", loginVM);
                }

                var result = await patientService.Login(loginVM);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }


                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is locked. Please try again later.");
                    return View("Login", loginVM);
                }

                ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
                return View("Login", loginVM);
            }

            [HttpGet]
            public IActionResult Register()
            {
                return View(new RegistrationVM());
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> RegisterUser(RegistrationVM registerVM)
            {
                if (!ModelState.IsValid)
                {
                    return View("Register", registerVM);
                }

                var result = await patientService.RegisterUserAsync(registerVM);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Register", registerVM);
            }


            public async Task<IActionResult> Logout()
            {
                await patientService.Logout();
                return RedirectToAction("Index", "Home");
            }



            [HttpGet]
            public IActionResult ResetPassword(string token, string email)
            {
                if (token == null || email == null)
                {
                    ModelState.AddModelError("", "Invalid password reset token.");
                }

                var model = new ResetPasswordVM { Token = token, Email = email };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            public IActionResult ResetPasswordConfirmation()
            {
                TempData["ResetPasswordConfirmation"] = "Now you can LogIn";
                return View();
            }

            public IActionResult ExternalLogin(string provider, string returnUrl = "")
            {
                var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

                var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

                return new ChallengeResult(provider, properties);
            }

            public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "", string remoteError = "")
            {

                var loginVM = new LoginVM()
                {
                    Schemes = await _signInManager.GetExternalAuthenticationSchemesAsync()
                };

                if (!string.IsNullOrEmpty(remoteError))
                {
                    ModelState.AddModelError("", $"Error from extranal login provide: {remoteError}");
                    return View("Login", loginVM);
                }


                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    ModelState.AddModelError("", $"Error from extranal login provide: {remoteError}");
                    return View("Login", loginVM);
                }

                var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

                if (signInResult.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                    if (!string.IsNullOrEmpty(userEmail))
                    {
                        var user = await _userManager.FindByEmailAsync(userEmail);

                        if (user == null)
                        {
                            user = new Patient()
                            {
                                UserName = userEmail,
                                Email = userEmail,
                                EmailConfirmed = true

                            };

                            await _userManager.CreateAsync(user);
                            await _userManager.AddToRoleAsync(user, "Patient");
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        TempData["ExternalLoginCallback"] = "Please,Complete the information";
                        return RedirectToAction("UpdateProfile");
                    }

                }

                ModelState.AddModelError("", $"Something went wrong");
                return View("Login", loginVM);
            }
            public async Task<IActionResult> ProfileInfo()
            {
                var model = await patientService.GetPatientForMoreInfo(User);
                if (model == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View(model);
            }
        }
    
}

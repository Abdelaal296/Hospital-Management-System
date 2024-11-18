using HospitalSystem.Models;
using HospitalSystem.Repo.Abstraction;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HospitalSystem.Repo.Impelementation
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<Patient> userManager;
        private readonly SignInManager<Patient> signInManager;

        public AccountRepo(UserManager<Patient> userManager, SignInManager<Patient> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
 
        public async Task<Patient> GetPatientAsync(ClaimsPrincipal user)
        {
            return await userManager.GetUserAsync(user);
        }

        public async Task<IdentityResult> UpdatePatientAsync(Patient patient)
        {
            var result = await userManager.UpdateAsync(patient);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(patient);
            }

            return result;
        }

      
        // log out
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<Patient> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        //  CheckPassword
        public async Task<bool> CheckPasswordAsync(Patient user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
        //  ChangePassword
        public async Task<IdentityResult> ChangePasswordAsync(Patient user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task RefreshSignInAsync(Patient user)
        {
            await signInManager.RefreshSignInAsync(user);
        }

        public async Task<SignInResult> PasswordSignInAsync(Patient patient, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(patient, password, isPersistent, lockoutOnFailure);
        }

        public async Task<IdentityResult> AccessFailedAsync(Patient patient)
        {
            return await userManager.AccessFailedAsync(patient);
        }

        public async Task<bool> IsLockedOutAsync(Patient patient)
        {
            return await userManager.IsLockedOutAsync(patient);
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return await signInManager.GetExternalAuthenticationSchemesAsync();
        }
        public async Task<IdentityResult> CreateUserAsync(Patient user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task AddToRoleAsync(Patient user, string role)
        {
            await userManager.AddToRoleAsync(user, role);
        }

        public async Task<SignInResult> PasswordSignInAsync(Patient user, string password)
        {
            return await signInManager.PasswordSignInAsync(user, password, false, false);
        }
    }

}

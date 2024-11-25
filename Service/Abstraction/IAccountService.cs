﻿using HospitalSystem.Models;
using HospitalSystem.ModelVM.Account;
using HospitalSystem.ModelVM.Patient;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HospitalSystem.Service.Abstraction
{
    public interface IAccountService
    {
        Task<PatientProfileVM> GetProfile(ClaimsPrincipal user);
        Task<EditePatientVM> GetPatientForEdit(ClaimsPrincipal user);
        Task<PatientVM> GetPatientForMoreInfo(ClaimsPrincipal user);
        Task<IdentityResult> UpdatePatient(ClaimsPrincipal user, EditePatientVM model);
        Task<Patient> GetCurrentPatient(ClaimsPrincipal user);
        Task<IdentityResult> DeletePatientAccount(ClaimsPrincipal user);
        Task<IdentityResult> ChangePassword(ChangePasswordVM model);
        // Task<Patient> GetCurrentUser();
        Task<IdentityResult> UpdatePatient(Patient patient);
        Task<LoginVM> GetLoginViewModelAsync();
        Task<SignInResult> Login(LoginVM loginVM);
        Task<IdentityResult> RegisterUserAsync(RegistrationVM registerVM);
        Task Logout();
        Task<bool> IsLockedOut(Patient patient);
    }
}

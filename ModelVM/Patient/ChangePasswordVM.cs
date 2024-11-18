using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.ModelVM.Patient
{
    public class ChangePasswordVM
    {

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters long.")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}

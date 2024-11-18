using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.ModelVM.Account
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

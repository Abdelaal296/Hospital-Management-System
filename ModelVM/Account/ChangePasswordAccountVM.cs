﻿using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.ModelVM.Account
{
    public class ChangePasswordAccountVM
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
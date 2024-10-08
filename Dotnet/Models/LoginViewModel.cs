﻿using System.ComponentModel.DataAnnotations;

namespace FitnessAppcf.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        
        public string Email { get; set; }

        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }

}

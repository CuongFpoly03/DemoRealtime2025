using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime.Infrastructure.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name cannot be blank")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber cannot be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "PhoneNumber should contains numbers only")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "ConfirmPassword cannot be blank")]
        [Compare("Password", ErrorMessage = "ConfirmPassword and Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime.Infrastructure.DTOs.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public required string UserName {get; set;}
        [Required(ErrorMessage = "Password is required")]
        public required string Password {get; set;}
        
    }
}
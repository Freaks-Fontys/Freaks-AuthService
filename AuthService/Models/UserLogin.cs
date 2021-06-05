using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Models
{
    public class UserLogin
    {
        public String Email { get; set; }
        public String Password { get; set; }
    }
}

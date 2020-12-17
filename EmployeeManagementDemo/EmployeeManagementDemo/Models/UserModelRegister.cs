using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EmployeeManagementDemo.Models
{
    public class UserModelRegister
    {
        [Required(ErrorMessage = "Please enter Name.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password.")]
        [StringLength(15, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Re-enter Password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Please enter Root Password.")]
        public string RootPassword { get; set; }
    }
}
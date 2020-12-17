using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementDemo.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter Name.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }
    }
}
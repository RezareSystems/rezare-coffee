using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectCoffee.Models
{
    public class UserAccount
    {
        [Required(ErrorMessage = "UserName is required.", AllowEmptyStrings =false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
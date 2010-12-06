using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeerLearn.Data;

namespace PeerLearn.Web.Models
{
    public class ExistingUser
    {
        [Required]
        [StringLength(DataConstants.UserNameLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(DataConstants.PasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeerLearn.Data;
using PeerLearn.Web.Helpers;

namespace PeerLearn.Web.Models
{
    public class NewUser
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(DataConstants.UserNameLength)]
        [Remote("UserNameExists", "Account", "Username is already taken.")]
        [Display(Name="Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(DataConstants.EmailLength)]
        [Remote("EmailExists", "Account", "An account with this email address already exists.")]
        [DataType(DataType.EmailAddress)]
        [ValidEmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(DataConstants.PasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }
    }
}
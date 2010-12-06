using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PeerLearn.Web.Helpers
{
    public class ValidEmailAddressAttribute : ValidationAttribute
    {
        private const string EmailAddressRegex = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";

        public ValidEmailAddressAttribute()
        {
            //Default message unless declared on the attribute
            ErrorMessage = "{0} must be a valid email address";
        }

        public override bool IsValid(object value)
        {
            var stringValue = value as string;
            var r = new Regex(EmailAddressRegex, RegexOptions.IgnoreCase);
            return stringValue != null && r.IsMatch(stringValue);
        }
    }
}
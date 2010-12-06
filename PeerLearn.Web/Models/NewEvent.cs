using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PeerLearn.Data;

namespace PeerLearn.Web.Models
{
    public class NewEvent
    {
        public int EventId { get; set; }
        [Required]
        [StringLength(DataConstants.EventNameLength)]
        [Display(Name="Event Name")]
        public string EventName { get; set; }

        [Required]
        [Display(Name="Event Description")]
        public string EventDescription { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name="Start Time")]
        public DateTime EventBeginTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name="End Time")]
        public DateTime EventEndTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name="Registration Begins")]
        public DateTime RegistrationBegins { get; set; }

        [Required]
        [Display(Name="Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Address 2")]
        public string StreetAddress2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Postal / ZIP Code")]
        public string PostalCode { get; set; }
    }
}
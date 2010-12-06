using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeerLearn.Web.Models
{
    public class EventListItem
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventBeginTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public DateTime RegistrationBegins { get; set; }
        public int AttendeeCount { get; set; }
    }
}
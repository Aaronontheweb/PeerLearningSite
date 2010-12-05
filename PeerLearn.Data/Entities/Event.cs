using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerLearn.Data.Entities
{
    public class Event
    {
        virtual public int EventId { get; set; }
        virtual public string EventName { get; set; }
        virtual public string EventDescription { get; set; }
        virtual public DateTime RegistrationOpenDate { get; set; }
        virtual public DateTime EventBeginDate { get; set; }
        virtual public DateTime EventEndDate { get; set; }

        virtual public Address Address { get; set; }

        virtual public IList<User> Speakers { get; set; }
        virtual public IList<User> Organizers { get; set; }
        virtual public IList<User> Attendees { get; set; }

        public Event()
        {
            RegistrationOpenDate = DateTime.Now;
            EventBeginDate = RegistrationOpenDate + new TimeSpan(1, 0, 0, 0);
            EventEndDate = EventBeginDate + new TimeSpan(3, 0, 0);
        }

        public virtual void AddAttendee(User attendee)
        {
            attendee.EventsAttended.Add(this);
            Attendees.Add(attendee);
        }

        public virtual void RemoveAttendee(User attendee)
        {
            attendee.EventsAttended.Remove(this);
            Attendees.Remove(attendee);
        }

        public virtual void AddOrganizer(User organizer)
        {
            organizer.EventsOrganized.Add(this);
            Organizers.Add(organizer);
        }

        public virtual void RemoveOrganizer(User organizer)
        {
            organizer.EventsOrganized.Remove(this);
            Organizers.Remove(organizer);
        }

        public virtual void AddSpeaker(User speaker)
        {
            speaker.EventsPresented.Add(this);
            Speakers.Add(speaker);
        }

        public virtual void RemoveSpeaker(User speaker)
        {
            speaker.EventsPresented.Remove(this);
            Speakers.Remove(speaker);
        }
    }
}


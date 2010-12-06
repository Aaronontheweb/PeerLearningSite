using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeerLearn.Data;
using PeerLearn.Data.Entities;
using PeerLearn.Web.Models;

namespace PeerLearn.Web.Service
{
    public class EventService : IEventService
    {
        public IRepository Repository { get; private set;}

        public EventService(IRepository repository)
        {
            Repository = repository;
        }

        public IList<EventListItem> GetUpcomingEvents(DateTime targetTime)
        {
            var events = Repository.GetUpcomingEvents(targetTime);

            return ConvertDalEventsToEventList(events);
        }

        public Event GetEvent(int eventId)
        {
            return Repository.GetEventById(eventId);
        }

        public Event GetEvent(string eventName)
        {
            return Repository.GetEventByName(eventName);
        }

        protected static IList<EventListItem> ConvertDalEventsToEventList(IEnumerable<Event> events)
        {
            return events.Select(ConvertDalEventToEventListItem).ToList();
        }

        protected static EventListItem ConvertDalEventToEventListItem(Event eventItem)
        {
            return new EventListItem
                       {
                           EventId = eventItem.EventId,
                           EventName = eventItem.EventName,
                           EventBeginTime = eventItem.EventBeginDate,
                           EventEndTime = eventItem.EventEndDate,
                           RegistrationBegins = eventItem.RegistrationOpenDate,
                           EventDescription = eventItem.EventDescription//,
                           //AttendeeCount = eventItem.Attendees.Count
                       };
        }
    }
}
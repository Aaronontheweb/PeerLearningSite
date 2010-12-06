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
        protected IRepository _repository;

        public EventService(IRepository repository)
        {
            _repository = repository;
        }

        public IList<EventListItem> GetUpcomingEvents(DateTime targetTime)
        {
            var events = _repository.GetUpcomingEvents(targetTime);

            return ConvertDalEventsToEventList(events);
        }

        public Event GetEvent(int eventId)
        {
            return _repository.GetEventById(eventId);
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
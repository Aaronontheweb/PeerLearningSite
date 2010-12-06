using System;
using System.Collections.Generic;
using PeerLearn.Data.Entities;
using PeerLearn.Web.Models;

namespace PeerLearn.Web.Service
{
    public interface IEventService
    {
        IList<EventListItem> GetUpcomingEvents(DateTime targetTime);
        Event GetEvent(int eventId);
    }
}
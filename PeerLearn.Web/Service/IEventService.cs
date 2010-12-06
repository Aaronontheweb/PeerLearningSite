using System;
using System.Collections.Generic;
using PeerLearn.Data;
using PeerLearn.Data.Entities;
using PeerLearn.Web.Models;

namespace PeerLearn.Web.Service
{
    public interface IEventService
    {
        IRepository Repository { get; }
        IList<EventListItem> GetUpcomingEvents(DateTime targetTime);
        Event GetEvent(int eventId);
        Event GetEvent(string eventName);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data
{
    public interface IRepository
    {
        #region User methods
        User GetUserById(int id);
        User GetUserByName(string username);
        User GetUserByEmail(string email);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        #endregion

        #region Event methods
        Event GetEventById(int id);
        Event GetEventByName(string eventName);
        bool CreateEvent(Event newEvent);
        bool UpdateEvent(Event updatedEvent);
        bool DeleteEvent(Event deletedEvent);
        #endregion

        #region Collection methods

        IList<Event> GetUpcomingEvents(DateTime time);
        IList<Event> GetUpcomingEventsForUser(User user, DateTime time);
        IList<Event> SearchEvents(string query);
        IList<Event> GetAllEvents();
        IList<User> SearchUsers(string query);
        IList<User> GetAllUsers();
        IList<Role> GetAllRoles();

        #endregion

        
    }
}

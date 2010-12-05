﻿using System;
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
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        #endregion

        #region Event methods
        Event GetEventById(int id);
        void CreateEvent(Event newEvent);
        void UpdateEvent(Event updatedEvent);
        void DeleteEvent(Event deletedEvent);
        #endregion

        #region Collection methods

        IList<Event> GetUpcomingEvents(DateTime time);
        IList<Event> GetUpcomingEventsForUser(User user, DateTime time);
        IList<Event> SearchEvents(string query);
        IList<Event> GetAllEvents();
        IList<User> SearchUsers(string query);
        IList<User> GetAllUsers();

        #endregion
    }
}

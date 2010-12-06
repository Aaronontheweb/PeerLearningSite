using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data
{
    public class Repository : IRepository
    {
        private readonly ISessionFactory _factory;

        public Repository(ISessionFactory factory)
        {
            _factory = factory;
        }

        #region Implementation of IRepository

        public User GetUserById(int id)
        {
            User user = null;

            using(var session = _factory.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    user = session.CreateCriteria(typeof (User))
                        .Add(NHibernate.Criterion.Restrictions.Eq("UserId", id))
                        .UniqueResult<User>();
                    if (user != null)
                    {
                        NHibernateUtil.Initialize(user.EventsOrganized);
                        NHibernateUtil.Initialize(user.EventsPresented);
                        NHibernateUtil.Initialize(user.EventsAttended);
                    }
                }
            }

            return user;
        }

        public User GetUserByName(string username)
        {
            User user = null;

            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    user = session.CreateCriteria(typeof(User))
                        .Add(NHibernate.Criterion.Restrictions.Eq("UserName", username))
                        .UniqueResult<User>();
                    if (user != null)
                    {
                        NHibernateUtil.Initialize(user.EventsOrganized);
                        NHibernateUtil.Initialize(user.EventsPresented);
                        NHibernateUtil.Initialize(user.EventsAttended);
                    }
                }
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    user = session.CreateCriteria(typeof(User))
                        .Add(NHibernate.Criterion.Restrictions.Eq("Email", email))
                        .UniqueResult<User>();

                    if(user != null)
                    {
                        NHibernateUtil.Initialize(user.EventsOrganized);
                        NHibernateUtil.Initialize(user.EventsPresented);
                        NHibernateUtil.Initialize(user.EventsAttended);
                    }
                }
            }

            return user;
        }

        public bool CreateUser(User user)
        {
            using(var session = _factory.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(user);
                        transaction.Commit();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public bool UpdateUser(User user)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(user);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool DeleteUser(User user)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(user);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public Event GetEventById(int id)
        {
            Event selectedEvent = null;

            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    selectedEvent = session.CreateCriteria(typeof(Event))
                        .Add(NHibernate.Criterion.Restrictions.Eq("EventId", id))
                        .UniqueResult<Event>();

                    if (selectedEvent != null)
                    {
                        NHibernateUtil.Initialize(selectedEvent.Organizers);
                        NHibernateUtil.Initialize(selectedEvent.Speakers);
                        NHibernateUtil.Initialize(selectedEvent.Attendees);
                    }
                }
            }

            return selectedEvent;
        }

        public Event GetEventByName(string eventName)
        {
            Event selectedEvent = null;

            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    selectedEvent = session.CreateCriteria(typeof(Event))
                        .Add(NHibernate.Criterion.Restrictions.Like("EventName", eventName, MatchMode.Exact))
                        .UniqueResult<Event>();

                    if(selectedEvent != null)
                    {
                        NHibernateUtil.Initialize(selectedEvent.Organizers);
                        NHibernateUtil.Initialize(selectedEvent.Speakers);
                        NHibernateUtil.Initialize(selectedEvent.Attendees);
                    }
                    
                }
            }

            return selectedEvent;
        }

        public bool CreateEvent(Event newEvent)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(newEvent);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool UpdateEvent(Event updatedEvent)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(updatedEvent);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool DeleteEvent(Event deletedEvent)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(deletedEvent);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public IList<Event> GetUpcomingEvents(DateTime time)
        {
            IList<Event> events = null;

            using(var session = _factory.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    events = session.CreateCriteria(typeof(Event))
                        .Add(NHibernate.Criterion.Restrictions.Ge("EventBeginDate", time))
                        .Add(NHibernate.Criterion.Restrictions.Le("RegistrationOpenDate", time))
                        .List<Event>();
                }
            }

            return events;
        }

        public IList<Event> GetUpcomingEventsForUser(User user, DateTime time)
        {
            IList<Event> events = null;

            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    events = session.CreateCriteria(typeof(Event))
                        .Add(NHibernate.Criterion.Restrictions.Ge("EventBeginDate", time))
                        .Add(NHibernate.Criterion.Restrictions.Le("RegistrationOpenDate", time))
                        .List<Event>();
                }
            }

            return events;
        }

        public IList<Event> SearchEvents(string query)
        {
            throw new NotImplementedException();
        }

        public IList<Event> GetAllEvents()
        {
            IList<Event> events = null;

            using(var session = _factory.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    events = session.CreateCriteria(typeof (Event)).List<Event>();
                }
            }

            return events;
        }

        public IList<User> SearchUsers(string query)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAllUsers()
        {
            IList<User> user = null;

            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    user = session.CreateCriteria(typeof(User)).List<User>();
                }
            }

            return user;
        }

        public IList<Role> GetAllRoles()
        {
            IList<Role> roles = null;

            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    roles = session.CreateCriteria(typeof(Role)).List<Role>();
                    foreach(var role in roles)
                    {
                        NHibernateUtil.Initialize(role.UsersInRole);
                    }
                }
            }

            return roles;
        }

        #endregion
    }
}

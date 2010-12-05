using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
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
                    NHibernateUtil.Initialize(user.EventsOrganized);
                    NHibernateUtil.Initialize(user.EventsPresented);
                    NHibernateUtil.Initialize(user.EventsAttended);
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
                }
            }

            return user;
        }

        public void CreateUser(User user)
        {
            using(var session = _factory.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }

        public void DeleteUser(User user)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
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
                    NHibernateUtil.Initialize(selectedEvent.Organizers);
                    NHibernateUtil.Initialize(selectedEvent.Speakers);
                    NHibernateUtil.Initialize(selectedEvent.Attendees);
                }
            }

            return selectedEvent;
        }

        public void CreateEvent(Event newEvent)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(newEvent);
                    transaction.Commit();
                }
            }
        }

        public void UpdateEvent(Event updatedEvent)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(updatedEvent);
                    transaction.Commit();
                }
            }
        }

        public void DeleteEvent(Event deletedEvent)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(deletedEvent);
                    transaction.Commit();
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

        #endregion
    }
}

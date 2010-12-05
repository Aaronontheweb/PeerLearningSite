using System.Configuration;
using PeerLearn.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NHibernate;
using PeerLearn.Data.Entities;
using System.Collections.Generic;

namespace PeerLearn.Data.Tests
{
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RepositoryTest
    {
        private static DebugSessionFactoryContainer _container;
        private static Repository _repository;
        private static IList<User> _users;
        private static IList<Event> _events;
        private static IList<Role> _roles;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
       
        public static void BuildTestData()
        {
            _roles = new List<Role>
                         {
                             new Role {RoleName = "User", UsersInRole = new List<User>()},
                             new Role {RoleName = "Admin", UsersInRole = new List<User>()},
                             new Role {RoleName = "Organizer", UsersInRole = new List<User>()}
                         };

            
            _users = new List<User>
                         {
                             new User
                                 {
                                     UserName = "Aaronontheweb",
                                     Password = "cleartext password",
                                     Bio = "Something short",
                                     Blog = new Uri("http://www.aaronstannard.com/"),
                                     Website = new Uri("http://www.stannardlabs.com/"),
                                     DateJoined = new DateTime(2010, 9, 13),
                                     Email = "aaron@aaronstannard.com",
                                     EventsAttended = new List<Event>(),
                                     EventsOrganized = new List<Event>(),
                                     EventsPresented = new List<Event>(),
                                     Roles = new List<Role>(),
                                     IsEnabled = true,
                                     IsPublic = true,
                                 },
                             new User
                                 {
                                     UserName = "Testy",
                                     Password = "unencrypted password lol",
                                     Bio = "Another thing that is somewhat short",
                                     Blog = new Uri("http://blog.test.com/"),
                                     Website = new Uri("http://www.test.com/"),
                                     DateJoined = new DateTime(2010, 10, 22),
                                     Email = "testy@test.com",
                                     EventsAttended = new List<Event>(),
                                     EventsOrganized = new List<Event>(),
                                     EventsPresented = new List<Event>(),
                                     Roles = new List<Role>(),
                                     IsEnabled = true,
                                     IsPublic = true,
                                 }
                         };

            _users[0].AddRole(_roles[0]);
            _users[0].AddRole(_roles[1]);
            _users[0].AddRole(_roles[2]);
            _users[1].AddRole(_roles[0]);

            _events = new List<Event>
                          {
                              new Event
                                  {
                                      EventName = "Test Event #1",
                                      EventDescription =
                                          "This is a basic test event we're using for... guess what? Testing purposes. Huzzah!",
                                      RegistrationOpenDate = DateTime.Now - new TimeSpan(3, 0, 0),
                                      EventBeginDate = DateTime.Now + new TimeSpan(10, 0, 0, 0),
                                      EventEndDate = DateTime.Now + new TimeSpan(10, 2, 0, 0),
                                      Address = new Address
                                                    {
                                                        StreetAddress = "101 Test Lane",
                                                        City = "Anytown",
                                                        PostalCode = "90063",
                                                        State = "CA"
                                                    },
                                      Attendees = new List<User>(),
                                      Organizers = new List<User>(),
                                      Speakers = new List<User>()
                                  },
                              new Event
                                  {
                                      EventName = "Test Event #2",
                                      RegistrationOpenDate = DateTime.Now - new TimeSpan(4, 1, 0, 0),
                                      EventBeginDate = DateTime.Now + new TimeSpan(21, 0, 30, 0),
                                      EventEndDate = DateTime.Now + new TimeSpan(21, 2, 30, 0),
                                      EventDescription = "A less interesting test event :(",
                                      Address = new Address
                                                    {
                                                        StreetAddress = "2638 S. Barrington Ave",
                                                        StreetAddress2 = "Apt. 202",
                                                        City = "Los Angeles",
                                                        State = "CA",
                                                        PostalCode = "90064"
                                                    },
                                      Attendees = new List<User>(),
                                      Organizers = new List<User>(),
                                      Speakers = new List<User>()
                                  }
                          };

            _events[0].AddSpeaker(_users[0]);
            _events[0].AddOrganizer(_users[0]);
            _events[0].AddAttendee(_users[0]);
            _events[0].AddAttendee(_users[1]);

            _events[1].AddOrganizer(_users[0]);
            _events[1].AddAttendee(_users[0]);
            _events[1].AddAttendee(_users[1]);
        }

        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            _container = new DebugSessionFactoryContainer(ConfigurationManager.ConnectionStrings["PeerLearn"].ConnectionString);
            _repository = new Repository(_container.CreateSessionFactory());
            BuildTestData();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region Tests

        /// <summary>
        /// Test is used to determine if we've configured our schema export tools and NHibernate SessionFactoryContainer properly
        /// </summary>
        [TestMethod]
        public void CanPopulateTestDataWithSessionFactory()
        {
            var wasTransactionSuccessful = false;

            var sessionFactory = _container.CreateSessionFactory();
            using(var session = sessionFactory.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    foreach(var eventItem in _events)
                    {
                        session.SaveOrUpdate(eventItem);
                    }
                    transaction.Commit();
                    wasTransactionSuccessful = transaction.WasCommitted;
                }
            }

            Assert.IsTrue(wasTransactionSuccessful);
            Assert.IsTrue(_users[0].UserId != 0);
        }

        /// <summary>
        /// Test method to determine if we can successfully retrieve a user by their user ID
        /// </summary>
        [TestMethod]
        public void CanRetrieveUserWithRepositoryByUserId()
        {
            var userId = _users[0].UserId;
            var retrievedUser = _repository.GetUserById(userId);

            Assert.AreEqual(userId, retrievedUser.UserId, "User Ids should be equivalent!");
            Assert.AreEqual(_users[0].UserId, retrievedUser.UserId, "User Ids should be equivalent!");
        }

        /// <summary>
        /// Test method to determine if we can successfully retrieve a user by their user name
        /// </summary>
        [TestMethod]
        public void CanRetrieveUserWithRepositoryByUserName()
        {
            var userName = _users[0].UserName;
            var retrievedUser = _repository.GetUserByName(userName);

            Assert.AreEqual(userName, retrievedUser.UserName, "UserNames should be equivalent!");
            Assert.AreEqual(_users[0].UserName, retrievedUser.UserName, "UserNames should be equivalent!");
        }

        /// <summary>
        /// Test method to determine if we can successfully retrieve a user by their email address
        /// </summary>
        [TestMethod]
        public void CanRetrieveUserWithRepositoryByEmail()
        {
            var userEmail = _users[0].Email;
            var retrievedUser = _repository.GetUserByEmail(userEmail);

            Assert.AreEqual(userEmail, retrievedUser.Email, "User Emails should be equivalent!");
            Assert.AreEqual(_users[0].Email, retrievedUser.Email, "User Emails should be equivalent!");
        }

        /// <summary>
        /// Test method to determine if we can successfully retrieve add a new user
        /// </summary>
        [TestMethod]
        public void CanAddNewUserToRepository()
        {
            var user = new User
                           {
                               UserName = "Testy MkIII",
                               Bio = "HAR HAR HAR",
                               Email = "testy.iii@test.com",
                               Password = "Jargon",
                               DateJoined = DateTime.Now,
                               IsEnabled = true,
                               IsPublic = true,
                               EventsAttended = new List<Event>(),
                               EventsOrganized = new List<Event>(),
                               EventsPresented = new List<Event>(),
                               Roles = new List<Role>()
                           };

            user.AddRole(_roles[0]);

            _repository.CreateUser(user);
            var users = _repository.GetAllUsers();
            var confirmUser = _repository.GetUserById(user.UserId);

            Assert.IsTrue(users.Count > _users.Count);
            Assert.AreEqual(user.UserId, confirmUser.UserId, "User Ids should be equivalent!");
        }

        [TestMethod]
        public void CanUpdateUserInRepository()
        {
            var user = new User
            {
                UserName = "Testy MkIV",
                Bio = "Heyoooooo",
                Email = "testy.iv@test.com",
                Password = "CLEARTEXT",
                DateJoined = DateTime.Now,
                Blog = new Uri("http://blog.msdn.com/blog"),
                IsEnabled = true,
                IsPublic = true,
                EventsAttended = new List<Event>(),
                EventsOrganized = new List<Event>(),
                EventsPresented = new List<Event>(),
                Roles = new List<Role>()
            };

            user.AddRole(_roles[0]);
            

            _repository.CreateUser(user);

            var originalUserName = user.UserName.Clone();
            var newUserName = "Modified name";
            user.UserName = newUserName;

            _repository.UpdateUser(user);

            var equivalentUser = _repository.GetUserById(user.UserId);
            Assert.AreEqual(newUserName, equivalentUser.UserName, "UserNames should be equivalent");
        }

        [TestMethod]
        public void CanGetEventCountsForUser()
        {
            var user = _repository.GetUserById(_users[1].UserId);

            Assert.IsTrue(NHibernateUtil.IsInitialized(user.EventsAttended));
            Assert.IsTrue(NHibernateUtil.IsInitialized(user.EventsPresented));
            Assert.IsTrue(NHibernateUtil.IsInitialized(user.EventsOrganized));

            Assert.AreEqual(_users[1].EventsAttended.Count, user.EventsAttended.Count);
            Assert.AreEqual(_users[1].EventsPresented.Count, user.EventsPresented.Count);
            Assert.AreEqual(_users[1].EventsOrganized.Count, user.EventsOrganized.Count);
        }

        /// <summary>
        /// Test method to determine if we can successfully query an event from the repository by it's ID #
        /// </summary>
        [TestMethod]
        public void CanGetEventByIdFromRepository()
        {
            var eventId = _events[0].EventId;

            var retrievedEvent = _repository.GetEventById(eventId);

            Assert.IsTrue(retrievedEvent != null);
            Assert.AreEqual(eventId, retrievedEvent.EventId);
        }

        /// <summary>
        /// Determine if we can successfully query all of the event attendees out of the repository when
        /// performing a simple get event by eventId query.
        /// </summary>
        [TestMethod]
        public void CanGetEventAttendeesByEventIdFromRepository()
        {
            var expectedEvent = _events[0];

            var retrievedEvent = _repository.GetEventById(expectedEvent.EventId);

            Assert.AreEqual(expectedEvent.Attendees.Count, retrievedEvent.Attendees.Count, "Should have same number of attendees!");
            Assert.AreEqual(expectedEvent.Speakers.Count, retrievedEvent.Speakers.Count, "Should have same number of speakers!");
            Assert.AreEqual(expectedEvent.Organizers.Count, retrievedEvent.Organizers.Count, "Should have same number of organizers!");
        }

        [TestMethod]
        public void CanGetUpcomingEvents()
        {
            var targetDate = DateTime.Now;
            var upcomingEvents = _repository.GetUpcomingEvents(targetDate);

            Assert.IsTrue(upcomingEvents.Count > 0);
            Assert.AreEqual(_events.Count, upcomingEvents.Count);
        }

        [TestMethod]
        public void CantGetPastEventsFromUpcomingFunction()
        {
            var targetDate = DateTime.Now - new TimeSpan(365, 0, 0, 0);
            var upcomingEvents = _repository.GetUpcomingEvents(targetDate);

            Assert.IsTrue(upcomingEvents.Count == 0);
        }

        #endregion

    }
}

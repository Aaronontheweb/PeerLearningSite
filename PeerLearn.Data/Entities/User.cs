using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerLearn.Data.Entities
{
    public class User
    {
        virtual public int UserId { get; set; }
        virtual public string UserName { get; set; }
        virtual public string Password { get; set; }
        virtual public string FirstName { get; set; }
        virtual public string LastName { get; set; }
        virtual public string Email { get; set; }
        virtual public string TwitterHandle { get; set; }
        virtual public string Bio { get; set; }

        virtual public bool IsPublic { get; set; }
        virtual public bool IsEnabled { get; set; }

        virtual public Uri Blog { get; set; }
        virtual public Uri Website { get; set; }

        virtual public DateTime DateJoined { get; set; }

        virtual public IList<Role> Roles { get; set; }

        virtual public IList<Event> EventsAttended { get; set; }
        virtual public IList<Event> EventsOrganized { get; set; }
        virtual public IList<Event> EventsPresented { get; set; }

        public User()
        {
            Roles = new List<Role>();
            EventsAttended = new List<Event>();
            EventsOrganized = new List<Event>();
            EventsPresented = new List<Event>();
        }

        virtual public void AddRole(Role role)
        {
            role.UsersInRole.Add(this);
            Roles.Add(role);
        }

        virtual public void RemoveRole(Role role)
        {
            role.UsersInRole.Remove(this);
            Roles.Remove(role);
        }
    }
}

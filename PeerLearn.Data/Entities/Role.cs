using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerLearn.Data.Entities
{
    public class Role
    {
        virtual public int RoleId { get; set; }
        virtual public string RoleName { get; set; }

        virtual public IList<User> UsersInRole { get; set; }
    }
}

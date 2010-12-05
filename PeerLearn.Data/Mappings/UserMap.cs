using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.UserId).GeneratedBy.Identity();
            Map(x => x.Email).Unique().Length(DataConstants.EmailLength).Not.Nullable();
            Map(x => x.UserName).Unique().Length(DataConstants.UserNameLength).Not.Nullable();
            Map(x => x.Password).Length(DataConstants.PasswordLength).Not.Nullable();
            Map(x => x.Bio).Nullable();
            Map(x => x.DateJoined).Not.Nullable();
            Map(x => x.TwitterHandle).Nullable().Length(DataConstants.TwitterHandleLength);
            Map(x => x.FirstName).Nullable().Nullable().Length(DataConstants.FirstNameLength);
            Map(x => x.LastName).Length(DataConstants.LastNameLength);
            Map(x => x.Blog).Nullable();
            Map(x => x.Website).Nullable();
            Map(x => x.IsPublic).Not.Nullable();
            Map(x => x.IsEnabled).Not.Nullable();
            HasManyToMany(x => x.EventsAttended).Cascade.All().Inverse().Table(MapConstants.AttendeesTable);
            HasManyToMany(x => x.EventsOrganized).Cascade.All().Inverse().Table(MapConstants.OrganizersTable);
            HasManyToMany(x => x.EventsPresented).Cascade.All().Inverse().Table(MapConstants.SpeakersTable);
            HasManyToMany(x => x.Roles).Cascade.All().Table(MapConstants.RolesTable);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data.Mappings
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Id(x => x.EventId).GeneratedBy.Identity();
            Map(x => x.EventName).Unique().Length(DataConstants.EventNameLength).Not.Nullable();
            Map(x => x.EventDescription).Not.Nullable();
            Map(x => x.EventBeginDate).Not.Nullable();
            Map(x => x.EventEndDate).Not.Nullable();
            Map(x => x.RegistrationOpenDate).Not.Nullable();
            Component(x => x.Address);
            HasManyToMany(x => x.Attendees).Cascade.All().Table(MapConstants.AttendeesTable);
            HasManyToMany(x => x.Organizers).Cascade.All().Table(MapConstants.OrganizersTable);
            HasManyToMany(x => x.Speakers).Cascade.All().Table(MapConstants.SpeakersTable);
        }
    }
}

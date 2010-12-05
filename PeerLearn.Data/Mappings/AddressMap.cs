using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data.Mappings
{
    public class AddressMap : ComponentMap<Address>
    {
        public AddressMap()
        {
            Map(x => x.StreetAddress).Not.Nullable();
            Map(x => x.StreetAddress2).Nullable();
            Map(x => x.City).Not.Nullable();
            Map(x => x.State).Not.Nullable();
            Map(x => x.PostalCode).Not.Nullable();
        }
    }
}

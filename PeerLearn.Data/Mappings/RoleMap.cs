using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.RoleId).GeneratedBy.Identity();
            Map(x => x.RoleName).Unique().Length(DataConstants.RoleNameLength).Not.Nullable();
            HasManyToMany(x => x.UsersInRole).Cascade.All().Inverse().Table(MapConstants.RolesTable);
        }
    }
}

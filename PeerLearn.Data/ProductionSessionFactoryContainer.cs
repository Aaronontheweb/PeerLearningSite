using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using NHibernate;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data
{
    public class ProductionSessionFactoryContainer : ISessionFactoryContainer
    {

        public string ConnectionString
        {
            get; private set;
        }

        public ProductionSessionFactoryContainer(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString)
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Event>())
                .BuildSessionFactory();
        }

    }
}

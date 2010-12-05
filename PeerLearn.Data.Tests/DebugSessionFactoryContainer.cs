using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PeerLearn.Data.Entities;

namespace PeerLearn.Data.Tests
{
    public class DebugSessionFactoryContainer : ISessionFactoryContainer
    {
        public string ConnectionString { get; private set; }

        public DebugSessionFactoryContainer(string connection)
        {
            ConnectionString = connection;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString)
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Event>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
                .Create(false, true);
                //.Execute(true, true, true);
        }

    }
}

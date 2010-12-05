using NHibernate;

namespace PeerLearn.Data
{
    public interface ISessionFactoryContainer
    {
        string ConnectionString { get; }
        ISessionFactory CreateSessionFactory();
    }
}
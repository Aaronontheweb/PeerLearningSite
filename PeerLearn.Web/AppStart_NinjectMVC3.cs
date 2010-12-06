using System.Configuration;
using System.Web.Mvc;
using Ninject;
using Ninject.Mvc3;
using PeerLearn.Data;
using PeerLearn.Web.Service;

[assembly: WebActivator.PreApplicationStartMethod(typeof(PeerLearn.Web.AppStart_NinjectMVC3), "Start")]

namespace PeerLearn.Web {
    public static class AppStart_NinjectMVC3 {
        public static void RegisterServices(IKernel kernel) {
            //kernel.Bind<IThingRepository>().To<SqlThingRepository>();
            kernel.Bind<IRepository>().ToConstant(new Repository(
                                                      new ProductionSessionFactoryContainer(
                                                          ConfigurationManager.ConnectionStrings["PeerLearn"].
                                                              ConnectionString).CreateSessionFactory()));
            kernel.Bind<IEventService>().To<EventService>();
        }

        public static void Start() {
            // Create Ninject DI Kernel 
            IKernel kernel = new StandardKernel();

            // Register services with our Ninject DI Container
            RegisterServices(kernel);

            // Tell ASP.NET MVC 3 to use our Ninject DI Container 
            DependencyResolver.SetResolver(new NinjectServiceLocator(kernel));
        }
    }
}

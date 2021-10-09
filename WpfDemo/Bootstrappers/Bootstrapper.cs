using Prism.Events;
using Prism.Ioc;
using Unity;
using Unity.Lifetime;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.BusinessLogics.Implementations;
using WpfDemo.Repositories;

namespace WpfDemo.Bootstrappers
{
    public class Bootstrapper 
    {
        private static IUnityContainer container = new UnityContainer();

        public static void RegisterTypes()
        {
            container.RegisterType<ICustomerManager, CustomerManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(JsonRepository<>));
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>(); ;
        }
    }
}

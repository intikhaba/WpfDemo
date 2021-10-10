using System.Windows;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.BusinessLogics.Implementations;
using WpfDemo.Repositories;
using WpfDemo.UIModules;

namespace WpfDemo.Bootstrappers
{
    public class PrismStartup : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICustomerManager, CustomerManager>();
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.Register(typeof(IRepository<>), typeof(JsonRepository<>));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CustomerEntryModule>();
            moduleCatalog.AddModule<CustomerListModule>();
        }
    }
}

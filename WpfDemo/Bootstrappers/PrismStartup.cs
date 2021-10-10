using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
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
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CustomerEntryModule>();
            moduleCatalog.AddModule<CustomerListModule>();
        }
    }
}

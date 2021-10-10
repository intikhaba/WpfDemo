using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WpfDemo.Constants;
using WpfDemo.UserControls;

namespace WpfDemo.UIModules
{
    public class CustomerListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(UIConstants.RightRegion, typeof(CustomerList));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}

using System.Windows.Controls;
using Prism.Events;
using Prism.Ioc;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.ViewModels;

namespace WpfDemo.UserControls
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : UserControl
    {
        public CustomerList()
        {
            InitializeComponent();
            DataContext = new CustomerListViewModel(ContainerLocator.Container.Resolve<ICustomerManager>(), ContainerLocator.Container.Resolve<IEventAggregator>());
        }
    }
}

using System.Windows.Controls;
using Prism.Events;
using WpfDemo.Bootstrappers;
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
            DataContext = new CustomerListViewModel(Bootstrapper.Resolve<ICustomerManager>(), Bootstrapper.Resolve<IEventAggregator>());
        }
    }
}

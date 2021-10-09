using System.Windows;
using WpfDemo.Bootstrappers;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.ViewModels;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CustomerMainViewModel(Bootstrapper.Resolve<ICustomerManager>());
        }
    }
}

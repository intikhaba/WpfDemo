using System.Windows.Controls;
using Unity;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.ViewModels;

namespace WpfDemo.UserControls
{
    /// <summary>
    /// Interaction logic for CustomerEntry.xaml
    /// </summary>
    public partial class CustomerEntry : UserControl
    {
        //public static readonly DependencyProperty FirstNameProperty =
        // DependencyProperty.Register("FirstName", typeof(string), typeof(CustomerEntry), new
        //    PropertyMetadata("", new PropertyChangedCallback(OnFirstNameChanged)));

        //public string FirstName
        //{
        //    get { return (string)GetValue(FirstNameProperty); }
        //    set { SetValue(FirstNameProperty, value); }
        //}

        //private static void OnFirstNameChanged(DependencyObject d,
        //   DependencyPropertyChangedEventArgs e)
        //{
        //    var customerEntry = d as CustomerEntry;
        //    customerEntry.OnFirstNameChanged(e);
        //}

        //private void OnFirstNameChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    tbTest.Text = e.NewValue.ToString();
        //}

        public CustomerEntry()
        {
            InitializeComponent();
            //IUnityContainer container = new UnityContainer();
            //var customerManager = container.Resolve<ICustomerManager>();
            //DataContext = new CustomerEntryViewModel();
        }
    }
}

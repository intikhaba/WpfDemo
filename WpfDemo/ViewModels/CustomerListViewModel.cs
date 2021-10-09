using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Prism.Events;
using WpfDemo.Bootstrappers;
using WpfDemo.PubSubEvents;

namespace WpfDemo.ViewModels
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {
        private ICollectionView customersView;
        private CustomerViewModel selectedCustomer;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CustomerViewModel> customers;

        public CustomerListViewModel()
        {
            Customers = new ObservableCollection<CustomerViewModel>(Enumerable.Empty<CustomerViewModel>());
            var eventAggregator = Bootstrapper.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<CustomerSelectPubSubEvent>().Subscribe((c) =>
            {
                SelectedCustomer = c;
            });
            ResetCustomerView();
        }

        public ICollectionView CustomersView
        {
            get { return customersView; }
            set
            {
                customersView = value;
                NotifyPropertyChanged("CustomersView");
            }
        }

        public ObservableCollection<CustomerViewModel> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                ResetCustomerView();
            }
        }

        public CustomerViewModel SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (value != null)
                {
                    selectedCustomer = value.Copy();
                }
                else
                {
                    selectedCustomer = null;
                }
                NotifyPropertyChanged("SelectedCustomer");
            }
        }

        public void Unselect()
        {
            SelectedCustomer = null;
        }

        private void ResetCustomerView()
        {
            CustomersView = CollectionViewSource.GetDefaultView(Customers);
            CustomersView.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));
            //CustomersView = CollectionViewSource.GetDefaultView(customerView);
            CustomersView.Refresh();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

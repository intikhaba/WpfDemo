using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Prism.Events;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.Commands;
using WpfDemo.Converters;
using WpfDemo.PubSubEvents;

namespace WpfDemo.ViewModels
{
    public class CustomerEntryViewModel : INotifyPropertyChanged
    {
        private CustomerViewModel customer;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ICustomerManager customerManager;
        private readonly IEventAggregator eventAggregator;

        public CustomerEntryViewModel(ICustomerManager customerManager, IEventAggregator eventAggregator)
        {
            Customer = new CustomerViewModel();

            this.customerManager = customerManager;
            this.eventAggregator = eventAggregator;
            InitializeCommands();
            SubscribeEvents();
        }

        public CustomerViewModel Customer
        {
            get
            {
                return customer;
            }
            set
            {
                if (value != null)
                {
                    customer = value;
                    NotifyPropertyChanged(nameof(Customer));
                }
            }
        }

        public RelayCommand SaveCustomer { get; set; }

        private bool CanSaveCustomer
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Customer.FirstName) && !string.IsNullOrWhiteSpace(Customer.LastName)
                    && Customer.DateOfBirth.HasValue && !string.IsNullOrWhiteSpace(Customer.PanNo) && !string.IsNullOrWhiteSpace(Customer.AadharNo);
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SubscribeEvents()
        {
            eventAggregator.GetEvent<CustomerUpdateSelectPubSubEvent>().Subscribe((c) =>
            {
                Customer = c;
            });
        }

        private void HandleSaveCustomer()
        {
            Task.Factory.StartNew(() =>
            {
                customerManager.SaveCustomer(Customer.ToBusinessCustomer());
            }).ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    if (t.IsFaulted)
                    {
                        MessageBox.Show(t.Exception.Message);
                    }
                    else
                    {
                        eventAggregator.GetEvent<CustomerEntryPubSubEvent>().Publish(Customer);
                    }
                }
            });
        }

        private void InitializeCommands()
        {
            SaveCustomer = new RelayCommand((p) =>
            {
                HandleSaveCustomer();
            }, p => CanSaveCustomer);
        }
    }
}

using System.ComponentModel;
using Prism.Events;
using WpfDemo.Commands;
using WpfDemo.EventHandlers;
using WpfDemo.PubSubEvents;

namespace WpfDemo.ViewModels
{
    public class CustomerEntryViewModel : INotifyPropertyChanged
    {
        public event CustomerEntryEventHandler CustomerEntry;
        private CustomerViewModel customer;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IEventAggregator eventAggregator;

        public CustomerEntryViewModel(IEventAggregator eventAggregator)
        {
            Customer = new CustomerViewModel();

            this.eventAggregator = eventAggregator;

            SaveCustomer = new RelayCommand((p) =>
            {
                var customerEntryPubSubEvent = new CustomerEntryPubSubEvent();
                eventAggregator.GetEvent<CustomerEntryPubSubEvent>().Publish(Customer);
                //CustomerEntry?.Invoke(this, new CustomerEntryEventArgs(Customer));
                //Customer.Reset();
            }, (p) => CanSaveCustomer);

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
    }
}

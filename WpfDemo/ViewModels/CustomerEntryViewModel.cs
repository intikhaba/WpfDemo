using System.ComponentModel;
using Prism.Events;
using WpfDemo.Bootstrappers;
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

        public CustomerEntryViewModel()
        {
            Customer = new CustomerViewModel();

            //var eventAggregator = (IEventAggregator)Prism.Ioc.ContainerLocator.Container.Resolve(typeof(IEventAggregator));
            //eventAggregator.

            var eventAggregator = Bootstrapper.Resolve<IEventAggregator>();

            SaveCustomer = new RelayCommand((p) =>
            {
                var customerEntryPubSubEvent = new CustomerEntryPubSubEvent();
                eventAggregator.GetEvent<CustomerEntryPubSubEvent>().Publish(Customer);
                //CustomerEntry?.Invoke(this, new CustomerEntryEventArgs(Customer));
                //Customer.Reset();
            }, (p) => !string.IsNullOrWhiteSpace(Customer.FirstName) && !string.IsNullOrWhiteSpace(Customer.LastName)
            && Customer.DateOfBirth.HasValue && !string.IsNullOrWhiteSpace(Customer.PanNo) && !string.IsNullOrWhiteSpace(Customer.AadharNo));

            eventAggregator.GetEvent<CustomerUpdateSelectPubSubEvent>().Subscribe((c) =>
            {
                Customer = c;
            });
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

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Prism.Events;
using WpfDemo.Bootstrappers;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.Converters;
using WpfDemo.Loggers;
using WpfDemo.PubSubEvents;

namespace WpfDemo.ViewModels
{
    public class CustomerMainViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerManager customerManager;
        private SolidColorBrush customerEntryBackground;
        public event PropertyChangedEventHandler PropertyChanged;
        private string saveText;
        private readonly IEventAggregator eventAggregator = Bootstrapper.Resolve<IEventAggregator>();

        public CustomerMainViewModel(ICustomerManager customerManager)
        {
            FileLogger.Instance.Log("CustomerMainViewModel is started");
            this.customerManager = customerManager;//new CustomerManager();

            CustomerEntryViewModel = new CustomerEntryViewModel();
            CustomerEntryBackground = Brushes.Azure;
            SaveText = "Save";
            //CustomerEntryViewModel.CustomerEntry += CustomerEntryViewModel_CustomerEntry;

            GetCustomers(customerManager);

            eventAggregator.GetEvent<CustomerEntryPubSubEvent>().Subscribe(OnCustomerSave);
            eventAggregator.GetEvent<CustomerDeletePubSubEvent>().Subscribe(OnCustomerDelete);
        }

        public CustomerEntryViewModel CustomerEntryViewModel { get; set; }

        public CustomerListViewModel CustomerListViewModel { get; set; }

        public SolidColorBrush CustomerEntryBackground
        {
            get
            {
                return customerEntryBackground;
            }
            set
            {
                if (value != customerEntryBackground)
                {
                    customerEntryBackground = value;
                    NotifyPropertyChanged(nameof(CustomerEntryBackground));
                }
            }
        }

        public string SaveText
        {
            get
            {
                return saveText;
            }
            set
            {
                saveText = value;
                NotifyPropertyChanged(nameof(SaveText));
            }
        }

        private void GetCustomers(ICustomerManager customerManager)
        {
            List<CustomerViewModel> customers = customerManager.GetCustomers().Select(c => c.ToViewModelCustomer())
                            .ToList();

            if (CustomerListViewModel == null)
            {
                CustomerListViewModel = new CustomerListViewModel();
            }

            CustomerListViewModel.Customers = new System.Collections.ObjectModel.ObservableCollection<CustomerViewModel>(customers);
        }

        private void OnCustomerSave(CustomerViewModel c)
        {
            Task.Factory.StartNew(() =>
            {
                CustomerViewModel newCustomer = c.Copy();
                customerManager.SaveCustomer(newCustomer.ToBusinessCustomer());

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GetCustomers(customerManager);
                }), DispatcherPriority.Background);
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
                        MessageBox.Show("Customer is saved successfully.");
                        c.Reset();
                        this.CustomerListViewModel.Unselect();
                    }
                }
            });
        }

        private void OnCustomerDelete(CustomerViewModel c)
        {
            Task.Factory.StartNew(() =>
            {
                customerManager.DeleteCustomer(c.Id);

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GetCustomers(customerManager);
                }), DispatcherPriority.Background);
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
                        MessageBox.Show("Customer is deleted successfully.");
                        c.Reset();
                        this.CustomerListViewModel.Unselect();
                    }
                }
            });
        }

        private void CustomerEntryViewModel_CustomerEntry(object sender, EventHandlers.CustomerEntryEventArgs args)
        {
            CustomerViewModel newCustomer = args.Customer.Copy();
            CustomerListViewModel.Customers.Add(newCustomer);
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Prism.Events;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.Converters;
using WpfDemo.PubSubEvents;

namespace WpfDemo.ViewModels
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {
        private ICollectionView customersView;
        private CustomerViewModel selectedCustomer;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CustomerViewModel> customers;
        private readonly ICustomerManager customerManager;
        private readonly IEventAggregator eventAggregator;
        private const string FirstNameSortField = "FirstName";

        public CustomerListViewModel(ICustomerManager customerManager, IEventAggregator eventAggregator)
        {
            this.customerManager = customerManager;
            this.eventAggregator = eventAggregator;

            GetCustomers();
            SubscribeEvents();
            ResetCustomerView();
        }
        
        public ICollectionView CustomersView
        {
            get { return customersView; }
            set
            {
                customersView = value;
                NotifyPropertyChanged(nameof(CustomersView));
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
                NotifyPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public void Unselect()
        {
            SelectedCustomer = null;
        }

        private void ResetCustomerView()
        {
            CustomersView = CollectionViewSource.GetDefaultView(Customers);
            CustomersView.SortDescriptions.Add(new SortDescription(FirstNameSortField, ListSortDirection.Ascending));
            CustomersView.Refresh();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnCustomerSave(CustomerViewModel c)
        {
            Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GetCustomers();
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
                        HandleSuccessfulPostTask("Customer is saved successfully.", c);
                    }
                }
            });
        }

        private void OnCustomerDelete(CustomerViewModel c)
        {
            Task.Factory.StartNew(() =>
            {
                customerManager.DeleteCustomer(c.Id);
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
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            GetCustomers();
                        }), DispatcherPriority.Background);

                        HandleSuccessfulPostTask("Customer is deleted successfully.", c);
                    }
                }
            });
        }

        private void HandleSuccessfulPostTask(string message, CustomerViewModel customerViewModel)
        {
            MessageBox.Show(message);
            customerViewModel.Reset();
            Unselect();
        }

        private void SubscribeEvents()
        {
            eventAggregator.GetEvent<CustomerEntryPubSubEvent>().Subscribe(OnCustomerSave);
            eventAggregator.GetEvent<CustomerDeletePubSubEvent>().Subscribe(OnCustomerDelete);
            eventAggregator.GetEvent<CustomerSelectPubSubEvent>().Subscribe((c) =>
            {
                SelectedCustomer = c;
            });
        }

        private void GetCustomers()
        {
            List<CustomerViewModel> customers = customerManager.GetCustomers().Select(c => c.ToViewModelCustomer()).ToList();
            Customers = new ObservableCollection<CustomerViewModel>(customers);
        }
    }
}

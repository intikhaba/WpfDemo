using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Prism.Events;
using WpfDemo.Bootstrappers;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.Converters;
using WpfDemo.PubSubEvents;

namespace WpfDemo.ViewModels
{
    public class CustomerMainViewModel
    {
        private readonly ICustomerManager customerManager;

        public CustomerMainViewModel(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;//new CustomerManager();

            CustomerEntryViewModel = new CustomerEntryViewModel();
            //CustomerEntryViewModel.CustomerEntry += CustomerEntryViewModel_CustomerEntry;

            GetCustomers(customerManager);

            var eventAggregator = Bootstrapper.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<CustomerEntryPubSubEvent>().Subscribe(OnCustomerSave);
            eventAggregator.GetEvent<CustomerDeletePubSubEvent>().Subscribe(OnCustomerDelete);
            
        }

        public CustomerEntryViewModel CustomerEntryViewModel { get; set; }

        public CustomerListViewModel CustomerListViewModel { get; set; }

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
            //Customer newCustomer = c.Copy();
            //customerManager.SaveCustomer(newCustomer.ToBusinessCustomer());

            //GetCustomers(customerManager);

            //MessageBox.Show("Customer is saved successfully.");

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
    }
}

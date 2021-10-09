using System;
using WpfDemo.ViewModels;

namespace WpfDemo.EventHandlers
{
    public class CustomerEntryEventArgs : EventArgs
    {
        public CustomerEntryEventArgs(CustomerViewModel customer) : base()
        {
            this.Customer = customer;
        }

        public CustomerViewModel Customer { get; set; }
    }
}

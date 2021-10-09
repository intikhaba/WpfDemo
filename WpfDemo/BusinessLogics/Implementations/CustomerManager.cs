using System;
using System.Collections.Generic;
using WpfDemo.BusinessLogics.Contracts;
using WpfDemo.Loggers;
using WpfDemo.Repositories;

namespace WpfDemo.BusinessLogics.Implementations
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IRepository<BusinessModels.Customer> customerRepository;

        public CustomerManager(IRepository<BusinessModels.Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public BusinessModels.Customer GetCustomer(int id)
        {
            return customerRepository.Get(id);
        }

        public IEnumerable<BusinessModels.Customer> GetCustomers()
        {
            return customerRepository.GetAll();
        }

        public void SaveCustomer(BusinessModels.Customer customer)
        {
            if (customer == null) { throw new ArgumentNullException("Customer cannot be null or empty."); }

            customerRepository.Save(customer);
            FileLogger.Instance.Log("SaveCustomer is finished");
        }

        public void DeleteCustomer(int customerId)
        {
            customerRepository.Delete(customerId);
        }
    }
}

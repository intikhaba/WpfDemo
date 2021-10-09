using System.Collections.Generic;

namespace WpfDemo.BusinessLogics.Contracts
{
    public interface ICustomerManager
    {
        BusinessModels.Customer GetCustomer(int id);

        IEnumerable<BusinessModels.Customer> GetCustomers();

        void SaveCustomer(BusinessModels.Customer customer);

        void DeleteCustomer(int customerId);
    }
}

using System.Collections.Generic;

namespace WpfDemo.BusinessModels
{
    public class ParentEntity
    {
        public ParentEntity()
        {
            Customers = new List<Customer>();
        }
        public List<Customer> Customers { get; set; }
    }
}

using System;

namespace WpfDemo.BusinessModels
{
    public class Customer : IBusinessEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PanNo { get; set; }

        public string AadharNo { get; set; }

        public bool IsPrime { get; set; }

        public Customer Copy()
        {
            return new Customer
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                PanNo = PanNo,
                AadharNo = AadharNo,
                IsPrime = IsPrime
            };
        }

        public void CopyFrom(Customer customer)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            DateOfBirth = customer.DateOfBirth;
            PanNo = customer.PanNo;
            AadharNo = customer.AadharNo;
            IsPrime = customer.IsPrime;
        }
    }
}

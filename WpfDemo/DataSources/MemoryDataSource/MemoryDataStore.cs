using System;
using System.Collections.Generic;
using System.Linq;
using WpfDemo.BusinessModels;

namespace WpfDemo.DataSources.MemoryDataSource
{
    public static class MemoryDataStore 
    {
        private static List<Customer> Customers = InitializeCustomers();

        public static List<T> GetEntities<T>() where T : class, IBusinessEntity
        {
            if (typeof(T) == typeof(Customer))
            {
                return Customers as List<T>;
            }

            throw new Exception("This is not supported type.");
        }

        public static T GetEntity<T>(int id) where T : class, IBusinessEntity
        {
            if (typeof(T) == typeof(Customer))
            {
                var customer = Customers.Single(c => c.Id == id) as T;
                return customer;
            }

            throw new Exception("This is not supported type.");
        }

        public static void SaveEntity<T>(T t) where T : class, IBusinessEntity
        {
            if (typeof(T) == typeof(Customer))
            {
                Customer newCustomer = t as Customer;
                SaveCustomer(newCustomer);
                return;
            }

            throw new Exception("This is not supported type.");
        }

        public static void DeleteEntity<T>(int id) where T : class, IBusinessEntity
        {
            if (typeof(T) == typeof(Customer))
            {
                DeleteCustomer(id);
                return;
            }

            throw new Exception("This is not supported type.");
        }

        private static void SaveCustomer(Customer newCustomer)
        {
            if (newCustomer.Id > 0)
            {
                Customer existingCustomer = Customers.Single(e => e.Id == newCustomer.Id);
                existingCustomer.CopyFrom(newCustomer);
                return;
            }

            int id = Customers.Max(c => c.Id) + 1;
            newCustomer.Id = id;
            Customers.Add(newCustomer);
        }

        private static void DeleteCustomer(int customerId)
        {
            Customers.Remove(Customers.Single(e => e.Id == customerId));
        }

        private static List<Customer> InitializeCustomers()
        {
            return new List<Customer>(new[]
            {
                new Customer()
                {
                    Id = 1,
                    FirstName = "Sadik",
                    LastName = "Alam",
                    DateOfBirth = new DateTime(2012, 3, 12),
                    PanNo = "NA",
                    AadharNo = "Sadik Aadhar",
                    IsPrime = true
                },
                new Customer()
                {
                    Id = 2,
                    FirstName = "Feroz",
                    LastName = "Alam",
                    DateOfBirth = new DateTime(1985, 4, 7),
                    PanNo = "NA",
                    AadharNo = "Feroz Aadhar",
                    IsPrime = false
                }
            });
        }
    }
}

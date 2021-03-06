using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfDemo.BusinessModels;

namespace WpfDemo.DataSources.JsonDataSource
{
    public static class JsonDataStore
    {
        private const string JsonFileName = @"DataSources\JsonDataSource\JsonEntities.json";

        public static List<T> GetEntities<T>() where T : class, IBusinessEntity
        {
            if (typeof(T) == typeof(Customer))
            {
                using (var stream = File.Open(JsonFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    int length = (int)stream.Length;
                    byte[] bytes = new byte[length];
                    stream.Read(bytes, 0, length);
                    string jsonText = UTF8Encoding.UTF8.GetString(bytes);
                    var customers = JsonConvert.DeserializeObject<ParentEntity>(jsonText).Customers;
                    return customers as List<T>;
                }
            }

            throw new Exception("This is not supported type.");
        }

        public static T GetEntity<T>(int id) where T : class, IBusinessEntity
        {
            if (typeof(T) == typeof(Customer))
            {
                var customers = GetEntities<Customer>();
                var customer = customers.Single(c => c.Id == id) as T;
                return customer;
            }

            throw new Exception("This is not supported type.");
        }

        public static async Task SaveEntity<T>(T t) where T : class, IBusinessEntity
        {
            await Task.Run(() =>
            {
                if (typeof(T) == typeof(Customer))
                {
                    Customer newCustomer = t as Customer;
                    SaveCustomer(newCustomer);
                    return;
                }

                throw new Exception("This is not supported type.");
            });
        }

        public static async Task DeleteEntity<T>(int id) where T : class, IBusinessEntity
        {
            await Task.Run(() =>
            {
                if (typeof(T) == typeof(Customer))
                {
                    DeleteCustomer(id);
                    return;
                }

                throw new Exception("This is not supported type.");
            });
        }

        private static void SaveCustomer(Customer newCustomer)
        {
            var customers = GetEntities<Customer>();

            if (newCustomer.Id > 0)
            {
                Customer existingCustomer = customers.Single(e => e.Id == newCustomer.Id);
                existingCustomer.CopyFrom(newCustomer);
            }
            else
            {
                int id = !customers.Any() ? 1 : customers.Max(c => c.Id) + 1;
                newCustomer.Id = id;
                customers.Add(newCustomer);
            }

            var parentEntity = new ParentEntity
            {
                Customers = customers
            };

            SaveEntities(parentEntity);
        }

        private static void DeleteCustomer(int customerId)
        {
            var customers = GetEntities<Customer>();
            Customer existingCustomer = customers.Single(e => e.Id == customerId);
            customers = customers.Where(c => c.Id != customerId).ToList();

            var parentEntity = new ParentEntity
            {
                Customers = customers
            };

            SaveEntities(parentEntity);
        }

        private static void SaveEntities(ParentEntity parentEntity)
        {
            using (var stream = File.Open(JsonFileName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            using (var streamWriter = new StreamWriter(stream))
            using (var writer = new JsonTextWriter(streamWriter) { Formatting = Formatting.Indented })
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, parentEntity);
            }
        }
    }
}

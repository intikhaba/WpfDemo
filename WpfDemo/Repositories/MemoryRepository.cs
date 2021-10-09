using System.Collections.Generic;
using System.Threading;
using WpfDemo.BusinessModels;
using WpfDemo.DataSources.MemoryDataSource;

namespace WpfDemo.Repositories
{
    public class MemoryRepository<T> : IRepository<T> where T : class, IBusinessEntity
    {
        public T Get(int id)
        {
            return MemoryDataStore.GetEntity<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return MemoryDataStore.GetEntities<T>();  
        }

        public void Save(T t)
        {
            Thread.Sleep(2000);
            MemoryDataStore.SaveEntity(t);
        }

        public void Delete(int id)
        {
            MemoryDataStore.DeleteEntity<T>(id);
        }
    }
}

using System.Collections.Generic;
using WpfDemo.BusinessModels;
using WpfDemo.DataSources.JsonDataSource;

namespace WpfDemo.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : class, IBusinessEntity
    {
        public T Get(int id)
        {
            return JsonDataStore.GetEntity<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return JsonDataStore.GetEntities<T>();  
        }

        public void Save(T t)
        {
            JsonDataStore.SaveEntity(t);
        }

        public void Delete(int id)
        {
            JsonDataStore.DeleteEntity<T>(id);
        }
    }
}

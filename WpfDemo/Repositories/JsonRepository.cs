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

        public async void Save(T t)
        {
            await JsonDataStore.SaveEntity(t);
        }

        public async void Delete(int id)
        {
            await JsonDataStore.DeleteEntity<T>(id);
        }
    }
}

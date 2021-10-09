using System.Collections.Generic;
using WpfDemo.BusinessModels;

namespace WpfDemo.Repositories
{
    public interface IRepository<T> where T : IBusinessEntity
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        void Save(T t);

        void Delete(int id);
    }
}

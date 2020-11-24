using DomainObjects;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IRepository<TModel> where TModel : Identity
    {
        int Count();
        TModel Create(TModel item);
        void DeleteRange(System.Collections.Generic.IEnumerable<TModel> list);
        TModel GetById(int v);
        void Update(TModel item);
        IEnumerable<TModel> GetAll();
        void CreateMany(IEnumerable<TModel> list);
        void DeleteById(int id);
    }
}

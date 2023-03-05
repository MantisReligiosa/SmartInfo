using DomainObjects;

namespace SmartInfo.ServiceInterfaces;

public interface IRepository<TModel> where TModel : Identity
{
    int Count();
    TModel Create(TModel item);
    void DeleteByIds(IEnumerable<int> ids);
    void DeleteMany(IEnumerable<Identity> identities);
    TModel GetById(int v);
    void Update(TModel item);
    IEnumerable<TModel> GetAll();
    void CreateMany(IEnumerable<TModel> list);
    void DeleteById(int id);
}
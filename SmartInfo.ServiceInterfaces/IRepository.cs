using DomainObjects;

namespace SmartInfo.ServiceInterfaces;

public interface IRepository<TModel> where TModel : Identity
{
    int Count();
    TModel Create(TModel item);
    void DeleteByIds(IEnumerable<Guid> ids);
    void DeleteMany(IEnumerable<Identity> identities);
    TModel GetById(Guid v);
    void Update(TModel item);
    IEnumerable<TModel> GetAll();
    void CreateMany(IEnumerable<TModel> list);
    void DeleteById(Guid id);
}
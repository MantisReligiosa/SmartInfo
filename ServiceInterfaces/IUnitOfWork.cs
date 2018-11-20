using DomainObjects;

namespace ServiceInterfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
    }
}

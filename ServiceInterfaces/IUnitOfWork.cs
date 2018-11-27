using DomainObjects;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Parameter> Parameters { get; }
        IRepository<Display> Displays { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}

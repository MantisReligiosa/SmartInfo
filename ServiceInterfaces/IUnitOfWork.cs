using DomainObjects;
using DomainObjects.Blocks;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Parameter> Parameters { get; }
        IRepository<Display> Displays { get; }
        IRepository<DisplayBlock> DisplayBlocks { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}

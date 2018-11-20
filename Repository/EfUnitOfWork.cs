using DomainObjects;
using ServiceInterfaces;

namespace Repository
{
    internal class EfUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public EfUnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

            Users = new Repository<User>(_databaseContext);
        }

        public IRepository<User> Users { get; private set; }
    }
}
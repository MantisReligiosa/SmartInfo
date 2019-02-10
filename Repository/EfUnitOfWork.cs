using DomainObjects;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Repository.Repositories;
using ServiceInterfaces;
using System.Threading.Tasks;

namespace Repository
{
    internal class EfUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public EfUnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

            Users = new Repository<User>(_databaseContext);
            Parameters = new Repository<Parameter>(_databaseContext);
            Displays = new Repository<Display>(_databaseContext);
            DisplayBlocks = new DisplayBlockRepository(_databaseContext);
            TableBlockCellDetails = new Repository<TableBlockCellDetails>(_databaseContext);
        }

        public IRepository<User> Users { get; private set; }
        public IRepository<Parameter> Parameters { get; private set; }
        public IRepository<Display> Displays { get; private set; }
        public IRepository<DisplayBlock> DisplayBlocks { get; private set; }
        public IRepository<TableBlockCellDetails> TableBlockCellDetails { get; private set; }

        public int Complete()
        {
            return _databaseContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _databaseContext.SaveChangesAsync();
        }
    }
}
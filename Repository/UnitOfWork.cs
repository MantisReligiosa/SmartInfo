using DomainObjects;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Repository.Entities;
using Repository.Entities.DetailsEntities;
using Repository.Repositories;
using ServiceInterfaces;
using ServiceInterfaces.IRepositories;
using System.Threading.Tasks;

namespace Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

            Users = new UserRepository(_databaseContext);
            Parameters = new ParameterRepository(_databaseContext);
            Displays = new Repository<Display, DisplayEntity>(_databaseContext);
            DisplayBlocks = new DisplayBlockRepository(_databaseContext);
            TableBlockCellDetails = new Repository<TableBlockCellDetails, TableBlockCellDetailsEntity>(_databaseContext);
            DateTimeFormats = new Repository<DateTimeFormat, DateTimeFormatEntity>(_databaseContext);
            Scenes = new Repository<Scene, SceneEntity>(_databaseContext);
            TableBlockRowHeights = new Repository<TableBlockRowHeight, TableBlockRowHeightEntity>(_databaseContext);
            TableBlockColumnWidths = new Repository<TableBlockColumnWidth, TableBlockColumnWidthEntity>(_databaseContext);
        }

        public IUserRepository Users { get; private set; }
        public IParametersRepository Parameters { get; private set; }
        public IRepository<Display> Displays { get; private set; }
        public IDisplayBlockRepository DisplayBlocks { get; private set; }
        public IRepository<TableBlockCellDetails> TableBlockCellDetails { get; private set; }
        public IRepository<DateTimeFormat> DateTimeFormats { get; private set; }
        public IRepository<Scene> Scenes { get; private set; }
        public IRepository<TableBlockRowHeight> TableBlockRowHeights { get; private set; }
        public IRepository<TableBlockColumnWidth> TableBlockColumnWidths { get; private set; }

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
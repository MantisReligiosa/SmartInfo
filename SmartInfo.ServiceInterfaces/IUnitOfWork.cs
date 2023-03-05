using DomainObjects;
using DomainObjects.Blocks.Details;
using SmartInfo.ServiceInterfaces.IRepositories;

namespace SmartInfo.ServiceInterfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IParametersRepository Parameters { get; }
    IRepository<Display> Displays { get; }
    IDisplayBlockRepository DisplayBlocks { get; }
    IRepository<TableBlockCellDetails> TableBlockCellDetails { get; }
    IRepository<DateTimeFormat> DateTimeFormats { get; }
    IRepository<Scene> Scenes { get; }
    IRepository<TableBlockRowHeight> TableBlockRowHeights { get; }
    IRepository<TableBlockColumnWidth> TableBlockColumnWidths { get; }

    int Complete();
    Task<int> CompleteAsync();
}
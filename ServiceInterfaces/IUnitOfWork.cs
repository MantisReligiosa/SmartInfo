using DomainObjects;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Parameter> Parameters { get; }
        IRepository<Display> Displays { get; }
        IRepository<DisplayBlock> DisplayBlocks { get; }
        IRepository<TableBlockCellDetails> TableBlockCellDetails { get; }
        IRepository<DateTimeFormat> DateTimeFormats { get; }
        IRepository<MetablockFrame> MetablockFrames { get; }
        IRepository<TableBlockRowHeight> TableBlockRowHeights { get; }
        IRepository<TableBlockColumnWidth> TableBlockColumnWidths { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}

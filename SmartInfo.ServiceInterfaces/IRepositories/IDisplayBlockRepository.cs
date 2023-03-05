using DomainObjects.Blocks;

namespace SmartInfo.ServiceInterfaces.IRepositories;

public interface IDisplayBlockRepository : IRepository<DisplayBlock>
{
    IEnumerable<DisplayBlock> GetBlocksInScene(Guid sceneId);
    IEnumerable<DisplayBlock> GetAllNonScenaried();
    void DeleteAll();
}
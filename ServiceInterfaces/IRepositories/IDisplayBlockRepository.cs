using DomainObjects.Blocks;
using System;
using System.Collections.Generic;

namespace ServiceInterfaces.IRepositories
{
    public interface IDisplayBlockRepository : IRepository<DisplayBlock>
    {
        IEnumerable<DisplayBlock> GetBlocksInScene(int? sceneId);
        IEnumerable<DisplayBlock> GetAllNonScenaried();
        void DeleteAll();
    }
}

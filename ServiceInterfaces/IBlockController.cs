using DomainObjects.Blocks;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IBlockController
    {
        TextBlock AddTextBlock();
        IEnumerable<DisplayBlock> GetBlocks();
        void SaveTextBlock(TextBlock textBlock);
    }
}

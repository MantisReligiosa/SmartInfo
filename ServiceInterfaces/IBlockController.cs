using DomainObjects.Blocks;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IBlockController
    {
        TextBlock AddTextBlock();
        TableBlock AddTableBlock();
        PictureBlock AddPictureBlock();
        IEnumerable<DisplayBlock> GetBlocks();
        void SaveTextBlock(TextBlock textBlock);
        void SaveTableBlock(TableBlock block);
    }
}

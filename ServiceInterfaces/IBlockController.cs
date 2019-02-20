using DomainObjects.Blocks;
using System;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IBlockController
    {
        void SetBackground(string color);
        string GetBackground();


        TextBlock AddTextBlock();
        TableBlock AddTableBlock();
        PictureBlock AddPictureBlock();
        TextBlock CopyTextBlock(TextBlock block);
        TableBlock CopyTableBlock(TableBlock block);
        PictureBlock CopyPictureBlock(PictureBlock block);
        IEnumerable<DisplayBlock> GetBlocks();
        void SaveTextBlock(TextBlock textBlock);
        void SaveTableBlock(TableBlock block);
        void SavePictureBlock(PictureBlock block);
        void DeleteBlock(Guid id);
        void Cleanup();
        void MoveAndResizeBlock(Guid id, int height, int width, int left, int top);
    }
}

using DomainObjects.Blocks;
using System;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IBlockController
    {
        void SetBackground(string color);
        string GetBackground();


        TextBlock AddTextBlock(int? sceneId);
        TableBlock AddTableBlock(int? sceneId);
        PictureBlock AddPictureBlock(int? sceneId);
        DateTimeBlock AddDateTimeBlock(int? sceneId);
        MetaBlock AddMetaBlock();
        TextBlock CopyTextBlock(TextBlock block);
        TableBlock CopyTableBlock(TableBlock block);
        PictureBlock CopyPictureBlock(PictureBlock block);
        DateTimeBlock CopyDateTimeBlock(DateTimeBlock block);
        MetaBlock CopyMetabLock(MetaBlock block);
        IEnumerable<DisplayBlock> GetBlocks();
        TextBlock SaveTextBlock(TextBlock textBlock);
        TableBlock SaveTableBlock(TableBlock block);
        PictureBlock SavePictureBlock(PictureBlock block);
        DateTimeBlock SaveDateTimeBlock(DateTimeBlock block);
        MetaBlock SaveMetabLock(MetaBlock b);
        void DeleteBlock(int id);
        void Cleanup();
        void MoveAndResizeBlock(int id, int height, int width, int left, int top);
    }
}

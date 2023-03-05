using DomainObjects.Blocks;

namespace SmartInfo.ServiceInterfaces;

public interface IBlockController
{
    void SetBackground(string color);
    string GetBackground();
    TextBlock AddTextBlock(Guid sceneId);
    TableBlock AddTableBlock(Guid sceneId);
    PictureBlock AddPictureBlock(Guid sceneId);
    DateTimeBlock AddDateTimeBlock(Guid sceneId);
    Scenario AddScenario();
    TextBlock CopyTextBlock(TextBlock block);
    TableBlock CopyTableBlock(TableBlock block);
    PictureBlock CopyPictureBlock(PictureBlock block);
    DateTimeBlock CopyDateTimeBlock(DateTimeBlock block);
    Scenario CopyScenario(Scenario block);
    IEnumerable<DisplayBlock> GetBlocks();
    TextBlock SaveTextBlock(TextBlock textBlock);
    TableBlock SaveTableBlock(TableBlock block);
    PictureBlock SavePictureBlock(PictureBlock block);
    DateTimeBlock SaveDateTimeBlock(DateTimeBlock block);
    Scenario SaveScenario(Scenario scenario);
    void DeleteBlock(Guid id);
    void Cleanup();
    void MoveAndResizeBlock(Guid id, int height, int width, int left, int top);
}
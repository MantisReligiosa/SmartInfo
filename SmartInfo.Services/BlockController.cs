using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using DomainObjects.Parameters;
using SmartInfo.ServiceInterfaces;

namespace SmartInfo.Services;

public class BlockController : IBlockController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISystemController _systemController;

    public BlockController(ISystemController systemController,
        IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _systemController = systemController;
    }

    public void SetBackground(string color)
    {
        var backgroundColor = _unitOfWork.Parameters.BackgroundColor;
        if (backgroundColor == null)
        {
            _unitOfWork.Parameters.Create(new BackgroundColor
            {
                Value = color
            });
        }
        else
        {
            backgroundColor.Value = color;
            _unitOfWork.Parameters.Update(backgroundColor);
        }
        _unitOfWork.Complete();
    }

    public string GetBackground()
    {
        var backgroundColor = _unitOfWork.Parameters.BackgroundColor;
        return backgroundColor?.Value ?? string.Empty;
    }

    public TextBlock AddTextBlock(Guid sceneId)
    {
        var zIndex = GetNextZindexInScene(sceneId);
        var block = _unitOfWork.DisplayBlocks.Create(new TextBlock
        {
            Caption = $"TextBlock",
            Height = 50,
            Width = 200,
            SceneId = sceneId,
            Details = new()
            {
                BackColor = "#ffffff",
                TextColor = "#000000",
                FontName = _systemController.GetFonts().First(),
                FontSize = _systemController.GetFontSizes().First(),
                FontIndex = 1.5,
                Align = Align.Left,
                Bold = false,
                Italic = false
            },
            ZIndex = zIndex
        }) as TextBlock;
        _unitOfWork.Complete();
        return block;
    }

    public TableBlock AddTableBlock(Guid sceneId)
    {
        var zIndex = GetNextZindexInScene(sceneId);
        var block = _unitOfWork.DisplayBlocks.Create(new TableBlock
        {
            Caption = "TableBlock",
            Height = 200,
            Width = 200,
            SceneId = sceneId,
            Details = new()
            {
                FontName = _systemController.GetFonts().First(),
                FontSize = _systemController.GetFontSizes().First(),
                FontIndex = 1.5,
                HeaderDetails = new()
                {
                    Align = Align.Center,
                    BackColor = "#000000",
                    TextColor = "#ffffff",
                    Bold = true,
                    Italic = false,
                },
                EvenRowDetails = new()
                {
                    Align = Align.Left,
                    BackColor = "#ffffff",
                    TextColor = "#000000",
                    Bold = false,
                    Italic = true,
                },
                OddRowDetails = new ()
                {
                    Align = Align.Left,
                    BackColor = "#e6e6e6",
                    TextColor = "#000000",
                    Bold = false,
                    Italic = true,
                },
                Cells = new List<TableBlockCellDetails>(),
                TableBlockColumnWidths = new List<TableBlockColumnWidth>(),
                TableBlockRowHeights = new List<TableBlockRowHeight>()
            },
            ZIndex = zIndex
        }) as TableBlock;
        _unitOfWork.Complete();
        return block;
    }

    public PictureBlock AddPictureBlock(Guid sceneId)
    {
        var zIndex = GetNextZindexInScene(sceneId);
        var block = _unitOfWork.DisplayBlocks.Create(new PictureBlock
        {
            Caption = "PictureBlock",
            Height = 50,
            Width = 50,
            SceneId = sceneId,
            Details = new(),
            ZIndex = zIndex
        }) as PictureBlock;
        _unitOfWork.Complete();
        return block;
    }

    public DateTimeBlock AddDateTimeBlock(Guid sceneId)
    {
        var zIndex = GetNextZindexInScene(sceneId);
        var block = _unitOfWork.DisplayBlocks.Create(new DateTimeBlock
        {
            Caption = "DateTimeBlock",
            Height = 50,
            Width = 50,
            SceneId = sceneId,
            Details = new()
            {
                BackColor = "#ffffff",
                TextColor = "#000000",
                FontName = _systemController.GetFonts().First(),
                FontSize = _systemController.GetFontSizes().First(),
                FontIndex = 1.5,
                Align = Align.Left,
                Bold = false,
                Italic = false
            },
            ZIndex = zIndex
        }) as DateTimeBlock;
        _unitOfWork.Complete();
        return block;
    }

    public Scenario AddScenario()
    {
        var block = _unitOfWork.DisplayBlocks.Create(new Scenario
        {
            Caption = "Scenario",
            Height = 50,
            Width = 50,
            Details = new()
            {
                Scenes = new List<Scene>
                {
                    new()
                    {
                        Index = 1,
                        Duration = 5,
                        Name = "Scene1",
                        Blocks = new List<DisplayBlock>()
                    },
                    new()
                    {
                        Index = 2,
                        Duration = 5,
                        Name = "Scene2",
                        Blocks = new List<DisplayBlock>()
                    }
                }
            }
        }) as Scenario;
        _unitOfWork.Complete();
        return block;
    }

    public TextBlock SaveTextBlock(TextBlock textBlock)
    {
        if (!(_unitOfWork.DisplayBlocks.GetById(textBlock.Id) is TextBlock block))
        {
            _unitOfWork.DisplayBlocks.Create(textBlock);
        }
        else
        {
            _unitOfWork.DisplayBlocks.Update(textBlock);
        }
        _unitOfWork.Complete();
        return textBlock;
    }

    public TableBlock SaveTableBlock(TableBlock tableBlock)
    {
        if (!(_unitOfWork.DisplayBlocks.GetById(tableBlock.Id) is TableBlock block))
        {
            _unitOfWork.DisplayBlocks.Create(tableBlock);
        }
        else
        {
            _unitOfWork.DisplayBlocks.Update(tableBlock);
        }
        _unitOfWork.Complete();
        return tableBlock;

    }

    public Scenario SaveScenario(Scenario scenario)
    {
        if (!(_unitOfWork.DisplayBlocks.GetById(scenario.Id) is Scenario block))
        {
            var result =  _unitOfWork.DisplayBlocks.Create(scenario) as Scenario;
            _unitOfWork.Complete();
            return result;
        }
        else
        {
            _unitOfWork.DisplayBlocks.Update(scenario);
            _unitOfWork.Complete();
            return _unitOfWork.DisplayBlocks.GetById(scenario.Id) as Scenario;

        }
    }

    public PictureBlock SavePictureBlock(PictureBlock pictureBlock)
    {
        if (!(_unitOfWork.DisplayBlocks.GetById(pictureBlock.Id) is PictureBlock block))
        {
            _unitOfWork.DisplayBlocks.Create(pictureBlock);
        }
        else
        {
            _unitOfWork.DisplayBlocks.Update(pictureBlock);
        }
        _unitOfWork.Complete();
        return pictureBlock;

    }

    public DateTimeBlock SaveDateTimeBlock(DateTimeBlock dateTimeBlock)
    {
        if (dateTimeBlock.Details.Format != null)
        {
            var format = _unitOfWork.DateTimeFormats.GetById(dateTimeBlock.Details.Format.Id);
            dateTimeBlock.Details.Format = format;
        }
        if (!(_unitOfWork.DisplayBlocks.GetById(dateTimeBlock.Id) is DateTimeBlock block))
        {
            _unitOfWork.DisplayBlocks.Create(dateTimeBlock);
        }
        else
        {
            _unitOfWork.DisplayBlocks.Update(dateTimeBlock);
        }
        _unitOfWork.Complete();
        return dateTimeBlock;

    }

    public TextBlock CopyTextBlock(TextBlock source)
    {
        var block = _unitOfWork.DisplayBlocks.Create(new TextBlock(source)) as TextBlock;
        _unitOfWork.Complete();
        return block;
    }

    public TableBlock CopyTableBlock(TableBlock source)
    {
        var block = _unitOfWork.DisplayBlocks.Create(new TableBlock(source)) as TableBlock;
        _unitOfWork.Complete();
        return block;
    }

    public PictureBlock CopyPictureBlock(PictureBlock source)
    {
        var block = _unitOfWork.DisplayBlocks.Create(new PictureBlock(source)) as PictureBlock;
        _unitOfWork.Complete();
        return block;
    }

    public DateTimeBlock CopyDateTimeBlock(DateTimeBlock source)
    {
        if (source.Details.Format != null)
        {
            var format = _unitOfWork.DateTimeFormats.GetById(source.Details.Format.Id);
            source.Details.Format = format;
        }
        var block = _unitOfWork.DisplayBlocks.Create(new DateTimeBlock(source)) as DateTimeBlock;
        _unitOfWork.Complete();
        return block;
    }

    public IEnumerable<DisplayBlock> GetBlocks()
    {
        var result = _unitOfWork.DisplayBlocks.GetAllNonScenaried();
        return result;
    }

    public void DeleteBlock(Guid id)
    {
        _unitOfWork.DisplayBlocks.DeleteById(id);
        _unitOfWork.Complete();
    }

    public void Cleanup()
    {
        _unitOfWork.DisplayBlocks.DeleteAll();
        _unitOfWork.Complete();
    }

    public void MoveAndResizeBlock(Guid id, int height, int width, int left, int top)
    {
        var block = _unitOfWork.DisplayBlocks.GetById(id);
        if (block == null)
        {
            return;
        }
        block.Height = height;
        block.Width = width;
        block.Left = left;
        block.Top = top;
        _unitOfWork.DisplayBlocks.Update(block);
        _unitOfWork.Complete();
    }

    public Scenario CopyScenario(Scenario source)
    {
        var block = _unitOfWork.DisplayBlocks.Create(new Scenario(source)) as Scenario;
        _unitOfWork.Complete();
        return block;
    }

    private int GetNextZindexInScene(Guid sceneId)
    {
        var blocks = _unitOfWork.DisplayBlocks.GetBlocksInScene(sceneId);
        if (!blocks.Any())
            return 0;
        else
            return blocks.Max(b => b.ZIndex) + 1;
    }
}
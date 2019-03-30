using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using DomainObjects.Parameters;
using DomainObjects.Specifications;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
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
            var backgroundColor = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<BackgroundColor>())).FirstOrDefault();
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
            var backgroundColor = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<BackgroundColor>())).FirstOrDefault();
            return backgroundColor?.Value ?? string.Empty;
        }

        public TextBlock AddTextBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new TextBlock
            {
                Caption = $"TextBlock",
                Height = 50,
                Width = 200,
                Details = new TextBlockDetails
                {
                    BackColor = "#ffffff",
                    TextColor = "#000000",
                    FontName = _systemController.GetFonts().First(),
                    FontSize = _systemController.GetFontSizes().First(),
                    FontIndex = 1.5,
                    Align = Align.Left,
                    Bold = false,
                    Italic = false
                }
            }) as TextBlock;
            _unitOfWork.Complete();
            return block;
        }

        public TableBlock AddTableBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new TableBlock
            {
                Caption = "TableBlock",
                Height = 200,
                Width = 200,
                Details = new TableBlockDetails
                {
                    FontName = _systemController.GetFonts().First(),
                    FontSize = _systemController.GetFontSizes().First(),
                    FontIndex = 1.5,
                    HeaderDetails = new TableBlockRowDetails
                    {
                        Align = Align.Center,
                        BackColor = "#000000",
                        TextColor = "#ffffff",
                        Bold = true,
                        Italic = false,
                    },
                    EvenRowDetails = new TableBlockRowDetails
                    {
                        Align = Align.Left,
                        BackColor = "#ffffff",
                        TextColor = "#000000",
                        Bold = false,
                        Italic = true,
                    },
                    OddRowDetails = new TableBlockRowDetails
                    {
                        Align = Align.Left,
                        BackColor = "#e6e6e6",
                        TextColor = "#000000",
                        Bold = false,
                        Italic = true,
                    },
                    Cells = new List<TableBlockCellDetails>(),
                }
            }) as TableBlock;
            _unitOfWork.Complete();
            return block;
        }

        public PictureBlock AddPictureBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new PictureBlock
            {
                Caption = "PictureBlock",
                Height = 50,
                Width = 50,
                Details = new PictureBlockDetails()
            }) as PictureBlock;
            _unitOfWork.Complete();
            return block;
        }

        public DateTimeBlock AddDateTimeBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new DateTimeBlock
            {
                Caption = "DateTimeBlock",
                Height = 50,
                Width = 50,
                Details = new DateTimeBlockDetails
                {
                    BackColor = "#ffffff",
                    TextColor = "#000000",
                    FontName = _systemController.GetFonts().First(),
                    FontSize = _systemController.GetFontSizes().First(),
                    //Format = _systemController.GetDatetimeFormats().First(),
                    FontIndex = 1.5,
                    Align = Align.Left,
                    Bold = false,
                    Italic = false
                }
            }) as DateTimeBlock;
            _unitOfWork.Complete();
            return block;
        }

        public MetaBlock AddMetaBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new MetaBlock
            {
                Caption = "MetaBlock",
                Height = 50,
                Width = 50,
                Details = new MetaBlockDetails
                {

                }
            }) as MetaBlock;
            _unitOfWork.Complete();
            return block;
        }

        public void SaveTextBlock(TextBlock textBlock)
        {
            if (!(_unitOfWork.DisplayBlocks.Get(textBlock.Id) is TextBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(textBlock);
            }
            else
            {
                block.CopyFrom(textBlock);
                _unitOfWork.DisplayBlocks.Update(block);
            }
        }

        public void SaveTableBlock(TableBlock tableBlock)
        {
            if (!(_unitOfWork.DisplayBlocks.Get(tableBlock.Id) is TableBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(tableBlock);
            }
            else
            {
                //ToDo: нужен рефакторинг
                block.Height = tableBlock.Height;
                block.Left = tableBlock.Left;
                block.Top = tableBlock.Top;
                block.Width = tableBlock.Width;
                block.Details.FontName = tableBlock.Details.FontName;
                block.Details.FontSize = tableBlock.Details.FontSize;
                block.Details.FontIndex = tableBlock.Details.FontIndex;
                block.Details.EvenRowDetails.CopyFrom(tableBlock.Details.EvenRowDetails);
                block.Details.OddRowDetails.CopyFrom(tableBlock.Details.OddRowDetails);
                block.Details.HeaderDetails.CopyFrom(tableBlock.Details.HeaderDetails);

                var cellsToDelete = block.Details.Cells
                    .Where(dbCell => !tableBlock.Details.Cells.Any(cell => dbCell.Row.Equals(cell.Row) && dbCell.Column.Equals(cell.Column))).ToList();
                foreach (var cellToDelete in cellsToDelete)
                {
                    _unitOfWork.TableBlockCellDetails.Delete(cellToDelete.Id);
                }
                _unitOfWork.Complete();
                foreach (var cell in tableBlock.Details.Cells)
                {
                    var databaseCell = block.Details.Cells.FirstOrDefault(dbCell => dbCell.Row.Equals(cell.Row) && dbCell.Column.Equals(cell.Column));
                    if (databaseCell == null)
                    {
                        block.Details.Cells.Add(new TableBlockCellDetails(cell));
                    }
                    else
                    {
                        databaseCell.Value = cell.Value;
                    }
                }
                _unitOfWork.DisplayBlocks.Update(block);
            }
        }

        public void SavePictureBlock(PictureBlock pictureBlock)
        {
            if (!(_unitOfWork.DisplayBlocks.Get(pictureBlock.Id) is PictureBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(pictureBlock);
            }
            else
            {
                block.CopyFrom(pictureBlock);
                _unitOfWork.DisplayBlocks.Update(block);
            }
        }

        public void SaveDateTimeBlock(DateTimeBlock dateTimeBlock)
        {
            if (dateTimeBlock.Details.Format != null)
            {
                var format = _unitOfWork.DateTimeFormats.Get(dateTimeBlock.Details.Format.Id);
                dateTimeBlock.Details.Format = format;
            }
            if (!(_unitOfWork.DisplayBlocks.Get(dateTimeBlock.Id) is DateTimeBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(dateTimeBlock);
            }
            else
            {
                block.CopyFrom(dateTimeBlock);
                _unitOfWork.DisplayBlocks.Update(block);
            }
        }

        public TextBlock CopyTextBlock(TextBlock source)
        {
            var block = _unitOfWork.DisplayBlocks.Create(new TextBlock(source)) as TextBlock;
            return block;
        }

        public TableBlock CopyTableBlock(TableBlock source)
        {
            var block = _unitOfWork.DisplayBlocks.Create(new TableBlock(source)) as TableBlock;
            return block;
        }

        public PictureBlock CopyPictureBlock(PictureBlock source)
        {
            var block = _unitOfWork.DisplayBlocks.Create(new PictureBlock(source)) as PictureBlock;
            return block;
        }

        public DateTimeBlock CopyDateTimeBlock(DateTimeBlock source)
        {
            var block = _unitOfWork.DisplayBlocks.Create(new DateTimeBlock(source)) as DateTimeBlock;
            return block;
        }

        public IEnumerable<DisplayBlock> GetBlocks()
        {
            var result = _unitOfWork.DisplayBlocks.GetAll().Where(b => b.MetablockFrameId == null);
            return result;
        }

        public void DeleteBlock(Guid id)
        {
            _unitOfWork.DisplayBlocks.Delete(id);
        }

        public void Cleanup()
        {
            var blocks = _unitOfWork.DisplayBlocks.GetAll();
            _unitOfWork.DisplayBlocks.DeleteRange(blocks);
        }

        public void MoveAndResizeBlock(Guid id, int height, int width, int left, int top)
        {
            var block = _unitOfWork.DisplayBlocks.Get(id);
            if (block == null)
            {
                return;
            }
            block.Height = height;
            block.Width = width;
            block.Left = left;
            block.Top = top;
            _unitOfWork.Complete();
        }
    }
}

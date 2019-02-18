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
                Height = 50,
                Width = 200,
                Details = new TextBlockDetails
                {
                    //Text = "Текст",
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
                    Cells = new List<TableBlockCellDetails>
                    {
                        /*
                        new TableBlockCellDetails{Row=0, Column=0, Value = "Header1"},
                        new TableBlockCellDetails{Row=0, Column=1, Value = "Header2"},
                        new TableBlockCellDetails{Row=0, Column=2, Value = "Header3"},
                        new TableBlockCellDetails{Row=0, Column=3, Value = "Header4"},
                        new TableBlockCellDetails{Row=1, Column=0, Value = "Cell11"},
                        new TableBlockCellDetails{Row=1, Column=1, Value = "Cell12"},
                        new TableBlockCellDetails{Row=1, Column=2, Value = "Cell13"},
                        new TableBlockCellDetails{Row=1, Column=3, Value = "Cell14"},
                        new TableBlockCellDetails{Row=2, Column=0, Value = "Cell21"},
                        new TableBlockCellDetails{Row=2, Column=1, Value = "Cell22"},
                        new TableBlockCellDetails{Row=2, Column=2, Value = "Cell23"},
                        new TableBlockCellDetails{Row=2, Column=3, Value = "Cell24"},
                        new TableBlockCellDetails{Row=3, Column=0, Value = "Cell31"},
                        new TableBlockCellDetails{Row=3, Column=1, Value = "Cell32"},
                        new TableBlockCellDetails{Row=3, Column=2, Value = "Cell33"},
                        new TableBlockCellDetails{Row=3, Column=3, Value = "Cell34"}
                        */
                    },
                }
            }) as TableBlock;
            _unitOfWork.Complete();
            return block;
        }

        public PictureBlock AddPictureBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new PictureBlock
            {
                Height = 50,
                Width = 50,
                Details = new PictureBlockDetails()
            }) as PictureBlock;
            /*
            using (var stream = new MemoryStream())
            {
                Resources.DefImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                block.Details.Base64Image = Convert.ToBase64String(stream.ToArray());
            }
            */
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
            _unitOfWork.Complete();
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
            _unitOfWork.Complete();
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
            _unitOfWork.Complete();
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

        public IEnumerable<DisplayBlock> GetBlocks()
        {
            var result = _unitOfWork.DisplayBlocks.GetAll();
            return result;
        }

        public void DeleteBlock(Guid id)
        {
            _unitOfWork.DisplayBlocks.Delete(id);
            _unitOfWork.Complete();
        }

        public void Cleanup()
        {
            var blocks = _unitOfWork.DisplayBlocks.GetAll();
            _unitOfWork.DisplayBlocks.DeleteRange(blocks);
            _unitOfWork.Complete();
        }
    }
}

using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using DomainObjects.Parameters;
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

        public TextBlock AddTextBlock(int? sceneId)
        {
            var zIndex = GetNextZindexInScene(sceneId);
            var block = _unitOfWork.DisplayBlocks.Create(new TextBlock
            {
                Caption = $"TextBlock",
                Height = 50,
                Width = 200,
                SceneId = sceneId,
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
                },
                ZIndex = zIndex
            }) as TextBlock;
            _unitOfWork.Complete();
            return block;
        }

        public TableBlock AddTableBlock(int? sceneId)
        {
            var zIndex = GetNextZindexInScene(sceneId);
            var block = _unitOfWork.DisplayBlocks.Create(new TableBlock
            {
                Caption = "TableBlock",
                Height = 200,
                Width = 200,
                SceneId = sceneId,
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
                    TableBlockColumnWidths = new List<TableBlockColumnWidth>(),
                    TableBlockRowHeights = new List<TableBlockRowHeight>()
                },
                ZIndex = zIndex
            }) as TableBlock;
            _unitOfWork.Complete();
            return block;
        }

        public PictureBlock AddPictureBlock(int? sceneId)
        {
            var zIndex = GetNextZindexInScene(sceneId);
            var block = _unitOfWork.DisplayBlocks.Create(new PictureBlock
            {
                Caption = "PictureBlock",
                Height = 50,
                Width = 50,
                SceneId = sceneId,
                Details = new PictureBlockDetails(),
                ZIndex = zIndex
            }) as PictureBlock;
            _unitOfWork.Complete();
            return block;
        }

        public DateTimeBlock AddDateTimeBlock(int? sceneId)
        {
            var zIndex = GetNextZindexInScene(sceneId);
            var block = _unitOfWork.DisplayBlocks.Create(new DateTimeBlock
            {
                Caption = "DateTimeBlock",
                Height = 50,
                Width = 50,
                SceneId = sceneId,
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
                },
                ZIndex = zIndex
            }) as DateTimeBlock;
            _unitOfWork.Complete();
            return block;
        }

        public MetaBlock AddMetaBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new MetaBlock
            {
                Caption = "Scenario",
                Height = 50,
                Width = 50,
                Details = new MetaBlockDetails
                {
                    Frames = new List<MetablockFrame>
                    {
                        new MetablockFrame
                        {
                            Index = 1,
                            Duration = 5,
                            Name = "Scene1",
                            Blocks = new List<DisplayBlock>()
                        },
                        new MetablockFrame
                        {
                            Index = 2,
                            Duration = 5,
                            Name = "Scene2",
                            Blocks = new List<DisplayBlock>()
                        }
                    }
                }
            }) as MetaBlock;
            _unitOfWork.Complete();
            return block;
        }

        public TextBlock SaveTextBlock(TextBlock textBlock)
        {
            if (!(_unitOfWork.DisplayBlocks.GetById(textBlock.Id) is TextBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(textBlock);
                _unitOfWork.Complete();
                return textBlock;
            }
            else
            {
                block.CopyFrom(textBlock);
                _unitOfWork.DisplayBlocks.Update(block);
                _unitOfWork.Complete();
                return block;
            }
        }

        public TableBlock SaveTableBlock(TableBlock tableBlock)
        {
            if (!(_unitOfWork.DisplayBlocks.GetById(tableBlock.Id) is TableBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(tableBlock);
                _unitOfWork.Complete();
                return tableBlock;
            }
            else
            {
                //ToDo: нужен рефакторинг
                block.Height = tableBlock.Height;
                block.Left = tableBlock.Left;
                block.Top = tableBlock.Top;
                block.Width = tableBlock.Width;
                block.Caption = tableBlock.Caption;
                block.SceneId = tableBlock.SceneId;
                block.ZIndex = tableBlock.ZIndex;
                block.Details.FontName = tableBlock.Details.FontName;
                block.Details.FontSize = tableBlock.Details.FontSize;
                block.Details.FontIndex = tableBlock.Details.FontIndex;
                block.Details.EvenRowDetails.CopyFrom(tableBlock.Details.EvenRowDetails);
                block.Details.OddRowDetails.CopyFrom(tableBlock.Details.OddRowDetails);
                block.Details.HeaderDetails.CopyFrom(tableBlock.Details.HeaderDetails);

                var cellsToDelete = block.Details.Cells
                    .Where(dbCell => !tableBlock.Details.Cells.Any(cell => dbCell.Row.Equals(cell.Row) && dbCell.Column.Equals(cell.Column))).ToList();
                _unitOfWork.TableBlockCellDetails.DeleteRange(cellsToDelete);
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

                var rowHeightsToDelete = block.Details.TableBlockRowHeights
                    .Where(dbRowHeights => !tableBlock.Details.TableBlockRowHeights.Any(rh => dbRowHeights.Index.Equals(rh.Index))).ToList();
                _unitOfWork.TableBlockRowHeights.DeleteRange(rowHeightsToDelete);
                _unitOfWork.Complete();
                foreach (var rowHeight in tableBlock.Details.TableBlockRowHeights)
                {
                    var dbRowHeight = block.Details.TableBlockRowHeights.FirstOrDefault(rh => rowHeight.Index.Equals(rh.Index));
                    if (dbRowHeight == null)
                    {
                        block.Details.TableBlockRowHeights.Add(new TableBlockRowHeight(rowHeight ));
                    }
                    else
                    {
                        dbRowHeight.Value = rowHeight.Value;
                        dbRowHeight.Units = rowHeight.Units;
                    }
                }

                var columnWidthToDelete = block.Details.TableBlockColumnWidths
                    .Where(dbColumnWidth => !tableBlock.Details.TableBlockColumnWidths.Any(cw => dbColumnWidth.Index.Equals(cw.Index))).ToList();
                _unitOfWork.TableBlockColumnWidths.DeleteRange(columnWidthToDelete);
                _unitOfWork.Complete();
                foreach (var columnWidth in tableBlock.Details.TableBlockColumnWidths)
                {
                    var dbColumnWidth = block.Details.TableBlockColumnWidths.FirstOrDefault(cw => columnWidth.Index.Equals(cw.Index));
                    if (dbColumnWidth == null)
                    {
                        block.Details.TableBlockColumnWidths.Add(new TableBlockColumnWidth(columnWidth));
                    }
                    else
                    {
                        dbColumnWidth.Value = columnWidth.Value;
                        dbColumnWidth.Units = columnWidth.Units;
                    }
                }

                _unitOfWork.DisplayBlocks.Update(block);
                _unitOfWork.Complete();
                return block;
            }
        }

        public MetaBlock SaveMetabLock(MetaBlock metaBlock)
        {
            if (!(_unitOfWork.DisplayBlocks.GetById(metaBlock.Id) is MetaBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(metaBlock);
                _unitOfWork.Complete();
                return metaBlock;
            }
            else
            {
                block.Height = metaBlock.Height;
                block.Width = metaBlock.Width;
                block.ZIndex = metaBlock.ZIndex;
                block.Left = metaBlock.Left;
                block.Top = metaBlock.Top;
                block.Caption = metaBlock.Caption;
                var blocksToDelete = block.Details.Frames.SelectMany(f => f.Blocks ?? new List<DisplayBlock>()).Where(dbBlock => !metaBlock.Details.Frames.SelectMany(mf => mf.Blocks).Any(b => b.Id.Equals(dbBlock.Id))).ToList();
                _unitOfWork.DisplayBlocks.DeleteRange(blocksToDelete);
                var framesToDelete = block.Details.Frames.Where(dbFrame => !metaBlock.Details.Frames.Any(f => f.Id.Equals(dbFrame.Id))).ToList();
                _unitOfWork.MetablockFrames.DeleteRange(framesToDelete);
                foreach (var frame in metaBlock.Details.Frames)
                {
                    //if (frame.Id.Equals(new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)))
                    //{
                    //    frame.Id = Guid.NewGuid();
                    //}
                    var dbFrame = block.Details.Frames.FirstOrDefault(f => f.Id.Equals(frame.Id));
                    if (dbFrame == null)
                    {
                        block.Details.Frames.Add(frame);
                    }
                    else
                    {
                        dbFrame.Duration = frame.Duration;
                        dbFrame.Index = frame.Index;
                        dbFrame.Name = frame.Name;
                        dbFrame.DateToUse = frame.DateToUse;
                        if (frame.UseFromTime.HasValue)
                        {
                            var span = frame.UseFromTime.Value;
                            span -= new TimeSpan(span.Days, 0, 0, 0);
                            dbFrame.UseFromTime = span;
                        }
                        if (frame.UseToTime.HasValue)
                        {
                            var span = frame.UseToTime.Value;
                            span -= new TimeSpan(span.Days, 0, 0, 0);
                            dbFrame.UseToTime = span;
                        }
                        dbFrame.UseInDate = frame.UseInDate;
                        dbFrame.UseInDayOfWeek = frame.UseInDayOfWeek;
                        dbFrame.UseInFri = frame.UseInFri;
                        dbFrame.UseInMon = frame.UseInMon;
                        dbFrame.UseInSat = frame.UseInSat;
                        dbFrame.UseInSun = frame.UseInSun;
                        dbFrame.UseInThu = frame.UseInThu;
                        dbFrame.UseInTimeInterval = frame.UseInTimeInterval;
                        dbFrame.UseInTue = frame.UseInTue;
                        dbFrame.UseInWed = frame.UseInWed;

                        foreach (var subBlock in frame.Blocks)
                        {
                            var dbBlock = dbFrame.Blocks.FirstOrDefault(b => b.Id.Equals(subBlock.Id));
                            {
                                if (dbBlock == null)
                                {
                                    dbFrame.Blocks.Add(subBlock);
                                }
                            }
                        }
                    }
                }
                    _unitOfWork.DisplayBlocks.Update(block);
                    _unitOfWork.Complete();
                return block;
            }
        }

        public PictureBlock SavePictureBlock(PictureBlock pictureBlock)
        {
            if (!(_unitOfWork.DisplayBlocks.GetById(pictureBlock.Id) is PictureBlock block))
            {
                _unitOfWork.DisplayBlocks.Create(pictureBlock);
                _unitOfWork.Complete();
                return pictureBlock;
            }
            else
            {
                block.CopyFrom(pictureBlock);
                _unitOfWork.DisplayBlocks.Update(block);
                _unitOfWork.Complete();
                return block;
            }
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
                _unitOfWork.Complete();
                return dateTimeBlock;
            }
            else
            {
                block.CopyFrom(dateTimeBlock);
                _unitOfWork.DisplayBlocks.Update(block);
                _unitOfWork.Complete();
                return block;
            }
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

        public void DeleteBlock(int id)
        {
            _unitOfWork.DisplayBlocks.DeleteById(id);
            _unitOfWork.Complete();
        }

        public void Cleanup()
        {
            _unitOfWork.DisplayBlocks.DeleteAll();
            _unitOfWork.Complete();
        }

        public void MoveAndResizeBlock(int id, int height, int width, int left, int top)
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
            _unitOfWork.Complete();
        }

        public MetaBlock CopyMetabLock(MetaBlock source)
        {
            var block = _unitOfWork.DisplayBlocks.Create(new MetaBlock(source)) as MetaBlock;
            _unitOfWork.Complete();
            return block;
        }

        private int GetNextZindexInScene(int? sceneId)
        {
            var blocks = _unitOfWork.DisplayBlocks.GetBlocksInScene(sceneId);
            if (!blocks.Any())
                return 0;
            else
                return blocks.Max(b => b.ZIndex) + 1;
        }
    }
}

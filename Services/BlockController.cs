using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using ServiceInterfaces;
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

        public TextBlock AddTextBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new TextBlock
            {
                Height = 50,
                Width = 200,
                Details = new TextBlockDetails
                {
                    Text = "Текст",
                    BackColor = "#ffffff",
                    TextColor = "#000000",
                    FontName = _systemController.GetFonts().First(),
                    FontSize = _systemController.GetFontSizes().First(),
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
                    Cells = new List<TableBlockCellDetails>
                    {
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
                    },
                }
            }) as TableBlock;
            _unitOfWork.Complete();
            block.Details.HeaderDetails = new TableBlockRowDetails
            {
                Align = Align.Center,
                BackColor = "#000000",
                TextColor = "#ffffff",
                Bold = true,
                Italic = false,
                TableBlockDetails = block.Details
            };
            block.Details.EvenRowDetails = new TableBlockRowDetails
            {
                Align = Align.Left,
                BackColor = "#ffffff",
                TextColor = "#000000",
                Bold = false,
                Italic = true,
                TableBlockDetails = block.Details
            };
            block.Details.OddRowDetails = new TableBlockRowDetails
            {
                Align = Align.Left,
                BackColor = "#e6e6e6",
                TextColor = "#000000",
                Bold = false,
                Italic = true,
                TableBlockDetails = block.Details
            };
            _unitOfWork.Complete();
            return block;
        }

        public void SaveTextBlock(TextBlock textBlock)
        {
            var block = _unitOfWork.DisplayBlocks.Get(textBlock.Id) as TextBlock;
            block.Height = textBlock.Height;
            block.Left = textBlock.Left;
            block.Top = textBlock.Top;
            block.Width = textBlock.Width;
            block.Details.Text = textBlock.Details.Text;
            block.Details.BackColor = textBlock.Details.BackColor;
            block.Details.TextColor = textBlock.Details.TextColor;
            block.Details.FontName = textBlock.Details.FontName;
            block.Details.FontSize = textBlock.Details.FontSize;
            block.Details.Align = textBlock.Details.Align;
            block.Details.Italic = textBlock.Details.Italic;
            block.Details.Bold = textBlock.Details.Bold;
            _unitOfWork.DisplayBlocks.Update(block);
            _unitOfWork.Complete();
        }

        public IEnumerable<DisplayBlock> GetBlocks()
        {
            var result = _unitOfWork.DisplayBlocks.GetAll();
            return result;
        }
    }
}

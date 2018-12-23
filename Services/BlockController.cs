using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class BlockController : IBlockController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlockController(
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
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
                    FontName = "Arial",
                    FontSize = 8
                }
            }) as TextBlock;
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

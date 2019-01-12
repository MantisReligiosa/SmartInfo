using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class TableBlock : DisplayBlock
    {
        public TableBlock() { }

        public TableBlock(TableBlock source) : base(source)
        {
            Details = new TableBlockDetails
            {
                HeaderDetails = new TableBlockRowDetails(source.Details.HeaderDetails),
                EvenRowDetails = new TableBlockRowDetails(source.Details.EvenRowDetails),
                OddRowDetails = new TableBlockRowDetails(source.Details.OddRowDetails),
                FontName = source.Details.FontName,
                FontSize = source.Details.FontSize
            };
            foreach (var cell in source.Details.Cells)
            {
                Details.Cells.Add(new TableBlockCellDetails(cell));
            }
        }

        public TableBlockDetails Details { get; set; }
    }
}

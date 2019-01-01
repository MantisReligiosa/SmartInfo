using System.Collections.Generic;

namespace DomainObjects.Blocks.Details
{
    public class TableBlockDetails : BlockDetails
    {
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public TableBlockRowDetails HeaderDetails { get; set; }
        public TableBlockRowDetails EvenRowDetails { get; set; }
        public TableBlockRowDetails OddRowDetails { get; set; }
        public ICollection<TableBlockCellDetails> Cells { get; set; }
    }
}

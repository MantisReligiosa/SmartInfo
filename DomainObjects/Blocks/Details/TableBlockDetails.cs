using System.Collections.Generic;

namespace DomainObjects.Blocks.Details
{
    public class TableBlockDetails : Identity, ICopyable<TableBlockDetails>
    {
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public double FontIndex { get; set; }
        public TableBlockRowDetails HeaderDetails { get; set; }
        public TableBlockRowDetails EvenRowDetails { get; set; }
        public TableBlockRowDetails OddRowDetails { get; set; }
        public ICollection<TableBlockCellDetails> Cells { get; set; }

        public TableBlockDetails() { }

        public TableBlockDetails(TableBlockDetails source)
        {
            CopyFrom(source);
        }

        public void CopyFrom(TableBlockDetails source)
        {
            if (HeaderDetails == null)
            {
                HeaderDetails = new TableBlockRowDetails();
            }
            if (EvenRowDetails == null)
            {
                EvenRowDetails = new TableBlockRowDetails();
            }
            if (OddRowDetails == null)
            {
                OddRowDetails = new TableBlockRowDetails();
            }
            HeaderDetails.CopyFrom(source.HeaderDetails);
            EvenRowDetails.CopyFrom(source.EvenRowDetails);
            OddRowDetails.CopyFrom(source.OddRowDetails);
            FontName = source.FontName;
            FontSize = source.FontSize;
            FontIndex = source.FontIndex;
            Cells = new List<TableBlockCellDetails>();
            foreach (var cell in source.Cells)
            {
                Cells.Add(new TableBlockCellDetails(cell));
            }
        }
    }
}

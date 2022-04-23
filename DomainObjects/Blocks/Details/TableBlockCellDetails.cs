namespace DomainObjects.Blocks.Details
{
    public class TableBlockCellDetails : Identity
    {
        public TableBlockCellDetails() { }

        public TableBlockCellDetails(TableBlockCellDetails source)
        {
            Row = source.Row;
            Column = source.Column;
            Value = source.Value;
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }

        public TableBlockDetails TableBlockDetails { get; set; }
    }
}

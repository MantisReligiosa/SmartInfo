namespace DomainObjects.Blocks.Details
{
    public class TableBlockRowDetails : Identity, ICopyable<TableBlockRowDetails>
    {
        public TableBlockRowDetails() { }

        public TableBlockRowDetails(TableBlockRowDetails source)
        {
            CopyFrom(source);
        }

        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public Align Align { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }

        public void CopyFrom(TableBlockRowDetails source)
        {
            Align = source.Align;
            BackColor = source.BackColor;
            Bold = source.Bold;
            Italic = source.Italic;
            TextColor = source.TextColor;
        }
    }
}

namespace DomainObjects.Blocks.Details
{
    public class TableBlockRowDetails : Identity
    {
        public TableBlockRowDetails() { }

        public TableBlockRowDetails(TableBlockRowDetails source)
        {
            BackColor = source.BackColor;
            TextColor = source.TextColor;
            Align = source.Align;
            Italic = source.Italic;
            Bold = source.Bold;
        }

        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public Align Align { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
    }
}

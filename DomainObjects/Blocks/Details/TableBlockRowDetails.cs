namespace DomainObjects.Blocks.Details
{
    public class TableBlockRowDetails : Identity
    {
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public Align Align { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
        public TableBlockDetails TableBlockDetails { get; set; }
    }
}

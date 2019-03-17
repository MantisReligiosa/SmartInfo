namespace DomainObjects.Blocks.Details
{
    public class DateTimeBlockDetails : Identity, ICopyable<DateTimeBlockDetails>
    {
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public Align Align { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
        public double FontIndex { get; set; }
        public DateTimeFormat Format { get; set; }

        public DateTimeBlockDetails() { }

        public DateTimeBlockDetails(DateTimeBlockDetails source)
        {
            CopyFrom(source);
        }

        public void CopyFrom(DateTimeBlockDetails source)
        {
            Align = source.Align;
            BackColor = source.BackColor;
            Bold = source.Bold;
            FontName = source.FontName;
            FontSize = source.FontSize;
            FontIndex = source.FontIndex;
            Italic = source.Italic;
            TextColor = source.TextColor;
            Format = source.Format;
        }
    }
}

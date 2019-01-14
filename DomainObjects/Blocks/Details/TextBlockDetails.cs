namespace DomainObjects.Blocks.Details
{
    public class TextBlockDetails : BlockDetails
    {
        public string Text { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public Align Align { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
        public double FontIndex { get; set; }

        public TextBlockDetails() { }

        public TextBlockDetails(TextBlockDetails source)
        {
            Align = source.Align;
            BackColor = source.BackColor;
            Bold = source.Bold;
            FontName = source.FontName;
            FontSize = source.FontSize;
            FontIndex = source.FontIndex;
            Italic = source.Italic;
            Text = source.Text;
            TextColor = source.TextColor;
        }
    }
}

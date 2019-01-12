using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class TextBlock : DisplayBlock
    {
        public TextBlock() : base() { }

        public TextBlock(TextBlock source) : base(source)
        {
            Details = new TextBlockDetails
            {
                Align = source.Details.Align,
                BackColor = source.Details.BackColor,
                Bold = source.Details.Bold,
                FontName = source.Details.FontName,
                FontSize = source.Details.FontSize,
                Italic = source.Details.Italic,
                Text = source.Details.Text,
                TextColor = source.Details.TextColor
            };
        }

        public TextBlockDetails Details { get; set; }
    }
}

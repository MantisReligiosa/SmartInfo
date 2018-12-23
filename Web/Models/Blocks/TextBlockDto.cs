using DomainObjects.Blocks;

namespace Web.Models.Blocks
{
    public class TextBlockDto : BlockDto
    {
        public string Text { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public string Font { get; set; }
        public int FontSize { get; set; }
        public Align Align { get; internal set; }
    }
}
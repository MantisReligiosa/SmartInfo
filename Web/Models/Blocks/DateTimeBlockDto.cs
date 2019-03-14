using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;

namespace Web.Models.Blocks
{
    public class DateTimeBlockDto : BlockDto
    {
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public string Font { get; set; }
        public int FontSize { get; set; }
        public double FontIndex { get; set; }
        public Align Align { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
        public DateTimeFormatDetails Format { get; set; }
    }
}
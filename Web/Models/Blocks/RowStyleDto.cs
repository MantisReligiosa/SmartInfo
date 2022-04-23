using DomainObjects.Blocks;

namespace Web.Models.Blocks
{
    public class RowStyleDto
    {
        public int Id { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public Align Align { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
    }
}
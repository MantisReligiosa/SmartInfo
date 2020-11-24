using Repository.Entities.DisplayBlockEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DetailsEntities
{
    [Table("TextBlockDetails")]
    public class TextBlockDetailsEntity : Entity
    {
        [Column("Text")]
        public string Text { get; set; }

        [Column("TextColor")]
        public string TextColor { get; set; }

        [Column("BackColor")]
        public string BackColor { get; set; }

        [Column("FontName")]
        public string FontName { get; set; }

        [Column("FontSize")]
        public int FontSize { get; set; }

        [Column("Align")]
        public int Align { get; set; }

        [Column("Italic")]
        public int Italic { get; set; }

        [Column("Bold")]
        public int Bold { get; set; }

        [Column("FontIndex", TypeName ="decimal")]
        public decimal FontIndex { get; set; }

        public TextBlockEntity TextBlockEntity { get; set; }
    }
}

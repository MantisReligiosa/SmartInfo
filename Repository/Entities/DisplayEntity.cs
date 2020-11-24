using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("Displays")]
    public class DisplayEntity : Entity
    {
        [Column("Width")]
        public int Width { get; set; }

        [Column("Height")]
        public int Height { get; set; }

        [Column("Left")]
        public int Left { get; set; }

        [Column("Top")]
        public int Top { get; set; }
    }
}

using Repository.Entities.DisplayBlockEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("DisplayBlocks")]
    public class DisplayBlockEntity : Entity
    {
        [Column("Caption")]
        public string Caption { get; set; }

        [Column("Left")]
        public int Left { get; set; }

        [Column("Top")]
        public int Top { get; set; }

        [Column("Width")]
        public int Width { get; set; }

        [Column("Height")]
        public int Height { get; set; }

        [Column("Zindex")]
        public int ZIndex { get; set; }

        [Column("SceneId")]
        public int? SceneId { get; set; }

        public SceneEntity Scene { get; set; }
    }
}

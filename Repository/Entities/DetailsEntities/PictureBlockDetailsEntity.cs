using Repository.Entities.DisplayBlockEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DetailsEntities
{
    [Table("PictureBlockDetails")]
    public class PictureBlockDetailsEntity : Entity
    {
        [Column("Base64Image")]
        public string Base64Image { get; set; }
        
        [Column("ImageMode")]
        public int ImageMode { get; set; }

        [Column("SaveProportions")]
        public int SaveProportions { get; set; }

        public virtual PictureBlockEntity PictureBlockEntity { get; set; }
    }
}

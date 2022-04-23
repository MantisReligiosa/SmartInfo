using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DetailsEntities
{
    [Table("TableBlockRowDetails")]
    public class TableBlockRowDetailsEntity : Entity
    {
        [Column("TextColor")]
        public string TextColor { get; set; }

        [Column("BackColor")]
        public string BackColor { get; set; }

        [Column("Align")]
        public int Align { get; set; }

        [Column("Italic")]
        public int Italic { get; set; }

        [Column("Bold")]
        public int Bold { get; set; }

        [Column("TableBlockDetailsEntityId")]
        public int? TableBlockDetailsEntityId { get; set; }

        public virtual TableBlockDetailsEntity TableBlockDetailsEntity { get; set; }
    }
}

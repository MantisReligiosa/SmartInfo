using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DetailsEntities
{
    [Table("TableBlockRowHeights")]
    public class TableBlockRowHeightEntity : Entity
    {
        [Column("Index")]
        public int Index { get; set; }

        [Column("Value")]
        public int? Value { get; set; }

        [Column("Units")]
        public int Units { get; set; }

        [Column("TableBlockDetailsId")]
        public int TableBlockDetailsEntityId { get; set; }

        public virtual TableBlockDetailsEntity TableBlockDetailsEntity { get; set; }
    }
}

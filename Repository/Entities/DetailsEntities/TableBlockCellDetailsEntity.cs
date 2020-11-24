using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DetailsEntities
{
    [Table("TableCells")]
    public class TableBlockCellDetailsEntity : Entity
    {
        [Column("Row")]
        public int Row { get; set; }

        [Column("Column")]
        public int Column { get; set; }

        [Column("Value")]
        public string Value { get; set; }

        [Column("TableBlockDetailsId")]
        public int TableBlockDetailsEntityId { get; internal set; }

        public TableBlockDetailsEntity TableBlockDetailsEntity { get; set; }
    }
}

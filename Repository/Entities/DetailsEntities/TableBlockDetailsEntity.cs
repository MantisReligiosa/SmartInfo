using Repository.Entities.DisplayBlockEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DetailsEntities
{
    [Table("TableBlockDetails")]
    public class TableBlockDetailsEntity : Entity
    {
        [Column("FontName")]
        public string FontName { get; set; }

        [Column("FontSize")]
        public int FontSize { get; set; }

        [Column("FontIndex", TypeName = "decimal")]
        public decimal FontIndex { get; set; }

        public virtual TableBlockEntity TableBlockEntity { get; set; }

        public virtual ICollection<TableBlockRowDetailsEntity> RowDetailsEntities { get; set; }
        public virtual ICollection<TableBlockRowHeightEntity> RowHeightsEntities { get; set; }
        public virtual ICollection<TableBlockColumnWidthEntity> ColumnWidthEntities { get; set; }
        public virtual ICollection<TableBlockCellDetailsEntity> CellDetailsEntities { get; set; }
    }
}

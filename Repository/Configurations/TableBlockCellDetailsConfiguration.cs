using DomainObjects.Blocks.Details;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class TableBlockCellDetailsConfiguration : EntityTypeConfiguration<TableBlockCellDetails>
    {
        public TableBlockCellDetailsConfiguration()
        {
            ToTable("TableCells");
            HasKey(t => t.Id);
        }
    }
}

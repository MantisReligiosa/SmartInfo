using DomainObjects.Blocks.Details;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class TableBlockRowDetailsConfiguration : EntityTypeConfiguration<TableBlockRowDetails>
    {
        public TableBlockRowDetailsConfiguration()
        {
            ToTable("TableBlockRowDetails");
            HasKey(t => t.Id);
        }
    }
}

using DomainObjects.Blocks.Details;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class TableBlockDetailsConfiguration : EntityTypeConfiguration<TableBlockDetails>
    {
        public TableBlockDetailsConfiguration()
        {
            ToTable("TableBlockDetails");
            HasKey(t => t.Id);
            Property(t => t.FontName);
            Property(t => t.FontSize);

            //HasRequired(t => t.HeaderDetails).WithOptional(t => t.TableBlockDetails);

            HasMany(t => t.Cells)
                .WithRequired(c => c.TableBlockDetails)
                .HasForeignKey(c => c.TableBlockDetailsId);
        }
    }
}

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

            HasMany(t => t.Cells)
                .WithRequired(c => c.TableBlockDetails)
                .HasForeignKey(c => c.TableBlockDetailsId)
                .WillCascadeOnDelete(true);

            HasMany(t => t.TableBlockRowHeights)
                .WithRequired(c => c.TableBlockDetails)
                .HasForeignKey(c => c.TableBlockDetailsId)
                .WillCascadeOnDelete(true);

            HasMany(t => t.TableBlockColumnWidths)
                .WithRequired(c => c.TableBlockDetails)
                .HasForeignKey(c => c.TableBlockDetailsId)
                .WillCascadeOnDelete(true);
        }
    }
}

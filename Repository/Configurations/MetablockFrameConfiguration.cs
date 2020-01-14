using DomainObjects.Blocks.Details;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class MetablockFrameConfiguration : EntityTypeConfiguration<MetablockFrame>
    {
        public MetablockFrameConfiguration()
        {
            HasMany(t => t.Blocks)
                .WithRequired(c => c.MetablockFrame)
                .HasForeignKey(c => c.MetablockFrameId)
                .WillCascadeOnDelete(true);
        }
    }
}

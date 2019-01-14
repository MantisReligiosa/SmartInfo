using DomainObjects.Blocks;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class DisplayBlockConfiguration : EntityTypeConfiguration<DisplayBlock>
    {
        public DisplayBlockConfiguration()
        {
            ToTable("DisplayBlocks");
            HasKey(t => t.Id);
        }
    }
}

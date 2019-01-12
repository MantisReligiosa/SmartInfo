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
            Property(t => t.Left);
            Property(t => t.Top);
            Property(t => t.Width);
            Property(t => t.Height);
            Property(t => t.ZIndex);
        }
    }
}

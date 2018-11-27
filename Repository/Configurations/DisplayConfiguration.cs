using DomainObjects;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class DisplayConfiguration : EntityTypeConfiguration<Display>
    {
        public DisplayConfiguration()
        {
            ToTable("Displays");
            HasKey(u => u.Id);
            Property(u => u.Left);
            Property(u => u.Top);
            Property(u => u.Width);
            Property(u => u.Height);
        }
    }
}

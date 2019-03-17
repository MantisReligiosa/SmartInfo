using DomainObjects.Blocks.Details;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class DateTimeDetailsConfigurationConfiguration : EntityTypeConfiguration<DateTimeBlockDetails>
    {
        public DateTimeDetailsConfigurationConfiguration()
        {
            ToTable("DateTimeDetails");
            HasKey(t => t.Id);
        }
    }
}

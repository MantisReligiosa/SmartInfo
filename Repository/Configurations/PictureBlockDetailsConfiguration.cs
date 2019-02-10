using DomainObjects.Blocks.Details;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class PictureBlockDetailsConfiguration : EntityTypeConfiguration<PictureBlockDetails>
    {
        public PictureBlockDetailsConfiguration()
        {
            ToTable("PictureBlockDetailsDetails");
            HasKey(t => t.Id);
        }
    }
}

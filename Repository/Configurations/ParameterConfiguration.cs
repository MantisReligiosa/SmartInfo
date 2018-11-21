using DomainObjects;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class ParameterConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParameterConfiguration()
        {
            ToTable("Parameters");
            HasKey(u => u.Id);
            Property(u => u.Name);
            Property(u => u.Value);
        }
    }
}

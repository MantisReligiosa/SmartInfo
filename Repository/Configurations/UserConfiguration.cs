using DomainObjects;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            HasKey(u => u.Id);
        }
    }
}

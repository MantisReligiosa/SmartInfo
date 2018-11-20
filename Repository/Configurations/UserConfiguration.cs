using DomainObjects;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Profiles
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            HasKey(u => u.Id);
            Property(u => u.Login);
            Property(u => u.PasswordHash);
        }
    }
}

using DomainObjects;
using Repository.Configurations;
using System.Data.Entity;

namespace Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
        }

        public DatabaseContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
        }
    }
}

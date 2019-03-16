using DomainObjects;
using DomainObjects.Blocks;
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
        public DbSet<Display> Displays { get; set; }
        public DbSet<DisplayBlock> DisplayBlocks { get; set; }
        public DbSet<DateTimeFormatDetails> DateTimeFormatDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
            modelBuilder.Configurations.Add(new DisplayConfiguration());
            modelBuilder.Configurations.Add(new DisplayBlockConfiguration());
            modelBuilder.Configurations.Add(new TableBlockDetailsConfiguration());
            modelBuilder.Configurations.Add(new TableBlockCellDetailsConfiguration());
            modelBuilder.Configurations.Add(new TableBlockRowDetailsConfiguration());
        }
    }
}

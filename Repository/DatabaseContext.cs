using Repository.Entities;
using Repository.Entities.DetailsEntities;
using Repository.Entities.DetailsEntities.TableBlockRowDetailsEntities;
using Repository.Entities.DisplayBlockEntities;
using System.Data.Entity;

namespace Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ParameterEntity> Parameters { get; set; }
        public DbSet<DisplayEntity> Displays { get; set; }
        public DbSet<DisplayBlockEntity> DisplayBlocks { get; set; }
        public DbSet<DateTimeFormatEntity> DateTimeFormats { get; set; }
        public DbSet<SceneEntity> Scenes { get; set; }
        public DbSet<TextBlockDetailsEntity> TextBlockDetailsEntities { get; set; }
        public DbSet<PictureBlockDetailsEntity> PictureBlockDetailsEntities { get; set; }
        public DbSet<TableBlockDetailsEntity> TableBlockDetailsEntities { get; set; }
        public DbSet<TableBlockRowDetailsEntity> TableBlockRowDetailsEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DisplayBlockEntity>()
                .Map<TextBlockEntity>(m => m.Requires("Type").HasValue("Text"))
                .Map<PictureBlockEntity>(m => m.Requires("Type").HasValue("Picture"))
                .Map<DateTimeBlockEntity>(m => m.Requires("Type").HasValue("Datetime"))
                .Map<TableBlockEntity>(m => m.Requires("Type").HasValue("Table"));

            modelBuilder.Entity<TableBlockRowDetailsEntity>()
                .Map<TableBlockHeaderDetailsEntity>(m => m.Requires("Type").HasValue("Header"))
                .Map<TableBlockEvenRowDetailsEntity>(m => m.Requires("Type").HasValue("EvenRow"))
                .Map<TableBlockOddRowDetailsEntity>(m => m.Requires("Type").HasValue("OddRow"));

            modelBuilder.Entity<TextBlockEntity>()
                .HasRequired(e => e.TextBlockDetails).WithRequiredPrincipal(e => e.TextBlockEntity).Map(a => a.MapKey("TextBlockId"));
            modelBuilder.Entity<PictureBlockEntity>()
                .HasRequired(e => e.PictureBlockDetails).WithRequiredPrincipal(e => e.PictureBlockEntity).Map(a => a.MapKey("PictureBlockId"));
            modelBuilder.Entity<DateTimeBlockEntity>()
                .HasRequired(e => e.DateTimeBlockDetails).WithRequiredPrincipal(e => e.DatetimeBlockEntity).Map(a => a.MapKey("DateTimeBlockId"));
            modelBuilder.Entity<TableBlockEntity>()
                .HasRequired(e => e.TableBlockDetails).WithRequiredPrincipal(e => e.TableBlockEntity).Map(a => a.MapKey("TableBlockId"));

            modelBuilder.Entity<DateTimeBlockDetailsEntity>()
                .HasRequired(d => d.DateTimeFormatEntity).WithMany(f => f.DatetTimeBlockDetailsEntities).HasForeignKey(f => f.DateTimeFormatId);

            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.RowDetailsEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId);
            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.RowHeightsEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId);
            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.ColumnWidthEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId);
            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.CellDetailsEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId);
        }
    }
}

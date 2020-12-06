using Repository.Entities;
using Repository.Entities.DetailsEntities;
using Repository.Entities.DetailsEntities.TableBlockRowDetailsEntities;
using Repository.Entities.DisplayBlockEntities;
using Repository.Entities.ParameterEntities;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
            //Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ParameterEntity> Parameters { get; set; }
        public DbSet<DisplayEntity> Displays { get; set; }
        public DbSet<DisplayBlockEntity> DisplayBlocks { get; set; }
        public DbSet<DateTimeFormatEntity> DateTimeFormats { get; set; }
        public DbSet<ScenarioDetailsEntity> ScenarioDetailsEntities { get; set; }
        public DbSet<SceneEntity> Scenes { get; set; }
        public DbSet<TextBlockDetailsEntity> TextBlockDetailsEntities { get; set; }
        public DbSet<PictureBlockDetailsEntity> PictureBlockDetailsEntities { get; set; }
        public DbSet<TableBlockDetailsEntity> TableBlockDetailsEntities { get; set; }
        public DbSet<TableBlockRowDetailsEntity> TableBlockRowDetailsEntities { get; set; }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public int Count<TEntity>() where TEntity : class
        {
            return Set<TEntity>().AsQueryable().Count();
        }

        public TEntity Find<TEntity>(object id) where TEntity : class
        {
            return Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }


        public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetWith<TEntity>(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            var query = Set<TEntity>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.Where(expression);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public void SetDeletedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void SetModifiedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }


        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return Set<TEntity>().Single(expression);
        }

        public TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return Set<TEntity>().SingleOrDefault(expression);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParameterEntity>()
                .Map<BackgroundColorEntity>(m => m.Requires("Type").HasValue("BackgroundColor"))
                .Map<ScreenHeightEntity>(m => m.Requires("Type").HasValue("ScreenHeight"))
                .Map<ScreenWidthEntity>(m => m.Requires("Type").HasValue("ScreenWidth"));

            modelBuilder.Entity<DisplayBlockEntity>()
                .Map<TextBlockEntity>(m => m.Requires("Type").HasValue("Text"))
                .Map<PictureBlockEntity>(m => m.Requires("Type").HasValue("Picture"))
                .Map<DateTimeBlockEntity>(m => m.Requires("Type").HasValue("Datetime"))
                .Map<TableBlockEntity>(m => m.Requires("Type").HasValue("Table"))
                .Map<ScenarioEntity>(m => m.Requires("Type").HasValue("Scenario"));

            modelBuilder.Entity<TableBlockRowDetailsEntity>()
                .Map<TableBlockHeaderDetailsEntity>(m => m.Requires("Type").HasValue("Header"))
                .Map<TableBlockEvenRowDetailsEntity>(m => m.Requires("Type").HasValue("EvenRow"))
                .Map<TableBlockOddRowDetailsEntity>(m => m.Requires("Type").HasValue("OddRow"));

            modelBuilder.Entity<TextBlockEntity>()
                .HasRequired(e => e.TextBlockDetails).WithRequiredPrincipal(e => e.TextBlockEntity).Map(a => a.MapKey("TextBlockId")).WillCascadeOnDelete(true);
            modelBuilder.Entity<PictureBlockEntity>()
                .HasRequired(e => e.PictureBlockDetails).WithRequiredPrincipal(e => e.PictureBlockEntity).Map(a => a.MapKey("PictureBlockId")).WillCascadeOnDelete(true);
            modelBuilder.Entity<DateTimeBlockEntity>()
                .HasRequired(e => e.DateTimeBlockDetails).WithRequiredPrincipal(e => e.DatetimeBlockEntity).Map(a => a.MapKey("DateTimeBlockId")).WillCascadeOnDelete(true);
            modelBuilder.Entity<TableBlockEntity>()
                .HasRequired(e => e.TableBlockDetails).WithRequiredPrincipal(e => e.TableBlockEntity).Map(a => a.MapKey("TableBlockId")).WillCascadeOnDelete(true);
            modelBuilder.Entity<ScenarioEntity>()
                .HasRequired(e => e.ScenarioDetails).WithRequiredPrincipal(e => e.ScenarioEntity).Map(a => a.MapKey("ScenarioId")).WillCascadeOnDelete(true);

            modelBuilder.Entity<DateTimeBlockDetailsEntity>()
                .HasRequired(d => d.DateTimeFormatEntity).WithMany(f => f.DatetTimeBlockDetailsEntities).HasForeignKey(f => f.DateTimeFormatId);

            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.RowDetailsEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId).WillCascadeOnDelete(true);
            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.RowHeightsEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId).WillCascadeOnDelete(true);
            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.ColumnWidthEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId).WillCascadeOnDelete(true);
            modelBuilder.Entity<TableBlockDetailsEntity>()
                .HasMany(d => d.CellDetailsEntities).WithRequired(r => r.TableBlockDetailsEntity).HasForeignKey(f => f.TableBlockDetailsEntityId).WillCascadeOnDelete(true);

            modelBuilder.Entity<TableBlockRowDetailsEntity>().HasRequired(e => e.TableBlockDetailsEntity).WithMany().WillCascadeOnDelete(true);

            modelBuilder.Entity<ScenarioDetailsEntity>()
                .HasMany(s => s.Scenes).WithRequired(s => s.ScenarioDetailsEntity).HasForeignKey(f => f.ScenarioDetailsEntityId).WillCascadeOnDelete(true);

            modelBuilder.Entity<SceneEntity>()
                .HasRequired(e => e.ScenarioDetailsEntity).WithMany().WillCascadeOnDelete();

            modelBuilder.Entity<SceneEntity>()
                .HasMany(s => s.DisplayBlocks).WithOptional(s => s.Scene).HasForeignKey(s => s.SceneId).WillCascadeOnDelete(true);
        }
    }
}

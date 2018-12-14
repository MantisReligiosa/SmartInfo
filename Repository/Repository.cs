using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        internal readonly DatabaseContext Context;

        public Repository(DatabaseContext context)
        {
            Context = context;
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().CountAsync(predicate);
        }

        public TEntity Create(TEntity item)
        {
            return Context.Set<TEntity>().Add(item);
        }

        public TEntity Create()
        {
            return Context.Set<TEntity>().Create();
        }

        public void CreateMany(IEnumerable<TEntity> list)
        {
            Context.Set<TEntity>().AddRange(list);
        }

        public void Delete(object id)
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
                Context.Set<TEntity>().Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                if (!Context.Set<TEntity>().Local.Any(e => e == entity))
                    Context.Set<TEntity>().Attach(entity);
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            if (entitiesToDelete != null && entitiesToDelete.Count() > 0)
                Context.Set<TEntity>().RemoveRange(entitiesToDelete);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IEnumerable<TEntity> FindWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable<TEntity>();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query.Where(predicate);
        }

        public TEntity Get(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable<TEntity>();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query.ToList();
        }

        public IEnumerable<TEntity> GetAllWithInclude<T>(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable<TEntity>();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query.ToList();
        }

        public async Task<TEntity> GetAsync(object id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        bool IRepository<TEntity>.AnyWithInclude<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Context.Set<T>().AsQueryable<T>();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return Context.Set<T>().Any(predicate);
        }
    }
}

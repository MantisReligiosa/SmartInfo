using DomainObjects;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Identity
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

        public virtual TEntity Create(TEntity item)
        {
            return CreateItem(item);
        }

        protected TEntity CreateItem(TEntity item)
        {
            return Context.Set<TEntity>().Add(item);
        }

        public virtual void CreateMany(IEnumerable<TEntity> list)
        {
            Context.Set<TEntity>().AddRange(list);
        }

        public virtual void Delete(Guid id)
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
                Context.Set<TEntity>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> list)
        {
            if (list != null && list.Count() > 0)
                Context.Set<TEntity>().RemoveRange(list);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression);
        }

        public virtual TEntity Get(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual void Update(TEntity item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }
    }
}

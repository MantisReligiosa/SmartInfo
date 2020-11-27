using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceInterfaces
{
    public interface IDatabaseContext
    {
        int Count<TEntity>() where TEntity : class;
        
        IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        
        //IQueryable<TEntity> GetWith<TEntity>(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] p) where TEntity : class;
        
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;
        
        int SaveChanges();
        
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        
        TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        
        void SetDeletedState<TEntity>(TEntity entity) where TEntity : class;
        
        void SetModifiedState<TEntity>(TEntity entity ) where TEntity : class;
        
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        
        TEntity Find<TEntity>(object id) where TEntity : class;
        
        void Remove<TEntity>(TEntity entity) where TEntity : class;
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync();

        TEntity Get(object id);

        Task<TEntity> GetAsync(object id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> FindWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        TEntity Create(TEntity item);

        TEntity Create();

        void CreateMany(IEnumerable<TEntity> list);

        void Update(TEntity item);

        void Delete(object id);

        void DeleteRange(IEnumerable<TEntity> entitiesToDelete);

        void Delete(TEntity entity);

        bool AnyWithInclude<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class, TEntity;

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}

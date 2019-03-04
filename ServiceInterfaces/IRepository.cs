using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ServiceInterfaces
{
    public interface IRepository<TEntity> where TEntity : Identity
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(object id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

        TEntity Create(TEntity item);

        void CreateMany(IEnumerable<TEntity> list);

        void Update(TEntity item);

        void Delete(Guid id);

        void DeleteRange(IEnumerable<TEntity> list);

        int Count(Expression<Func<TEntity, bool>> expression);
    }
}

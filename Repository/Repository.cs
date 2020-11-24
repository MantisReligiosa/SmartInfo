using AutoMapper;
using DomainObjects;
using Repository.Entities;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository
{
    public class Repository<TModel, TEntity> : IRepository<TModel>
        where TModel : Identity
        where TEntity : Entity
    {
        internal readonly DatabaseContext Context;
        internal IMapper _mapper;

        public Repository(DatabaseContext context)
        {
            Context = context;
            var currentAssembly = Assembly.GetExecutingAssembly();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(currentAssembly);
            });

            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = mapperConfiguration.CreateMapper();
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public virtual TModel Create(TModel item)
        {
            return CreateItem(item);
        }

        protected TModel CreateItem(TModel item)
        {
            var entity = _mapper.Map<TModel, TEntity>(item);
            var addedEntity = Context.Set<TEntity>().Add(entity);
            var result = _mapper.Map<TEntity, TModel>(addedEntity);
            return result;
        }

        public virtual void CreateMany(IEnumerable<TModel> list)
        {
            Context.Set<TEntity>().AddRange(_mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(list));
        }

        public virtual void DeleteById(int id)
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
                Context.Set<TEntity>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TModel> list)
        {
            if (list != null && list.Count() > 0)
            {
                var entities = _mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(list);
                foreach(var entity in entities)
                {
                    Context.Entry(entity).State = EntityState.Deleted;
                }
                Context.Set<TEntity>().RemoveRange(entities);
            }
        }

        protected IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression);
        }

        public virtual TModel GetById(int id)
        {
            var entity =  Context.Set<TEntity>().Find(id);
            return _mapper.Map<TModel>(entity);
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            var entities = Context.Set<TEntity>().ToList();
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }

        public virtual void Update(TModel item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }
    }
}

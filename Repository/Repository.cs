using AutoMapper;
using DomainObjects;
using Repository.Entities;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository
{
    public class Repository<TModel, TEntity> : IRepository<TModel>
        where TModel : Identity
        where TEntity : Entity
    {
        internal readonly IDatabaseContext Context;
        internal IMapper _mapper;

        public Repository(IDatabaseContext context)
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
            return Context.Count<TEntity>();
        }

        public virtual TModel Create(TModel item)
        {
            var entity = _mapper.Map<TModel, TEntity>(item);
            var addedEntity = Context.Add(entity);
            Context.SaveChanges();
            var result = _mapper.Map<TEntity, TModel>(addedEntity);
            return result;
        }

        public virtual void CreateMany(IEnumerable<TModel> list)
        {
            Context.AddRange(_mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(list));
        }

        public virtual void DeleteById(int id)
        {
            TEntity entity = Context.Find<TEntity>(id);
            if (entity != null)
            {
                Context.Remove(entity);
            }
        }

        public virtual void DeleteByIds(IEnumerable<int> ids)
        {
            if (ids != null && ids.Count() > 0)
            {
                foreach (var id in ids)
                {
                    TEntity entity = Context.Find<TEntity>(id);
                    if (entity != null)
                    {
                        Context.Remove(entity);
                    }
                }
            }
        }

        public void DeleteMany(IEnumerable<Identity> identities)
        {
            DeleteByIds(identities.Select(i => i.Id));
        }

        protected IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Get(expression);
        }

        public virtual TModel GetById(int id)
        {
            var entity = Context.Find<TEntity>(id);
            return _mapper.Map<TModel>(entity);
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            var entities = Context.Get<TEntity>().ToList();
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }

        public virtual void Update(TModel item)
        {
            var entity = Context.Find<TEntity>(item.Id);
            _mapper.Map(item, entity);
        }
    }
}

using DomainObjects;
using DomainObjects.Parameters;
using Repository.Entities;
using Repository.Entities.ParameterEntities;
using Repository.Specifications;
using ServiceInterfaces;
using ServiceInterfaces.IRepositories;

namespace Repository.Repositories
{
    public class ParameterRepository : Repository<Parameter, ParameterEntity>, IParametersRepository
    {
        public ParameterRepository(IDatabaseContext context)
            : base(context)
        {
        }

        public ScreenHeight ScreenHeight => GetParameter<ScreenHeight, ScreenHeightEntity>();

        public ScreenWidth ScreenWidth => GetParameter<ScreenWidth, ScreenWidthEntity>();

        public BackgroundColor BackgroundColor => GetParameter<BackgroundColor, BackgroundColorEntity>();

        private TModel GetParameter<TModel, TEntity>()
            where TEntity : ParameterEntity
            where TModel : Parameter
        {
            var entity = (TEntity)Context.SingleOrDefault(ParameterSpecification.OfType<TEntity>());
            var parameter = _mapper.Map<TEntity,TModel>(entity);
            return parameter;
        }
    }
}

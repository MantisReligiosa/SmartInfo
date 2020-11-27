using AutoMapper;
using DomainObjects.Blocks;
using Repository.Entities.DisplayBlockEntities;

namespace Repository.Profiles.DisplayBlockEntitiesProfiles
{
    public class ScenarioEntityProfile : Profile
    {
        public ScenarioEntityProfile()
        {
            CreateMap<ScenarioEntity, Scenario>()
                .ForMember(model => model.Details, opt => opt.MapFrom(entity => entity.ScenarioDetails));

            CreateMap<Scenario, ScenarioEntity>()
                .ForMember(entity => entity.ScenarioDetails, opt => opt.MapFrom(model => model.Details))
                .AfterMap((model, entity) => entity.ScenarioDetails.ScenarioEntity = entity);
        }
    }
}

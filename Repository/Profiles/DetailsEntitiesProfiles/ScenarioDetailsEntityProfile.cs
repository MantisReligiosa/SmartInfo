using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class ScenarioDetailsEntityProfile : Profile
    {
        public ScenarioDetailsEntityProfile()
        {
            CreateMap<ScenarioDetailsEntity, ScenarioDetails>()
                .ForMember(model => model.Scenes, opt => opt.MapFrom(entity => entity.Scenes));

            CreateMap<ScenarioDetails, ScenarioDetailsEntity>()
                .ForMember(entity => entity.Scenes, opt => opt.MapFrom(model => model.Scenes))
                .ForMember(entity => entity.ScenarioEntity, opt => opt.Ignore())
                .ForMember(entity => entity.Id, opt => opt.Ignore());
        }
    }
}

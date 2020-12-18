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
                .ForMember(model => model.Scenes, opt => opt.MapFrom(entity => entity.SceneEntities));

            CreateMap<ScenarioDetails, ScenarioDetailsEntity>()
                .ForMember(entity => entity.SceneEntities, opt => opt.Ignore())
                .ForMember(entity => entity.ScenarioEntity, opt => opt.Ignore())
                .ForMember(entity => entity.Id, opt => opt.Ignore());
        }
    }
}

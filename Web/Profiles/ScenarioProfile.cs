using AutoMapper;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Web.Models.Blocks;

namespace Web.Profiles
{
    public class ScenarioProfile : Profile
    {
        public ScenarioProfile()
        {
            CreateMap<Scenario, ScenarioDto>()
                .ForMember(b => b.Type, opt => opt.MapFrom(b =>BlockType.Meta))
                .ForMember(b => b.Caption, opt => opt.MapFrom(b => string.IsNullOrEmpty(b.Caption) ? "meta" : b.Caption))
                .ForMember(b => b.Scenes, opt => opt.MapFrom(b => b.Details.Scenes));

            CreateMap<ScenarioDto, Scenario>()
                .ForPath(b => b.Details.Scenes, opt => opt.MapFrom(b => b.Scenes));

            CreateMap<Scene, SceneDto>();

            CreateMap<SceneDto, Scene>()
                .ForMember(dto => dto.Blocks, opt => opt.Ignore());
        }
    }
}
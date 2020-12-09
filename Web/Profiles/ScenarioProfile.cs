using AutoMapper;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Web.Models.Blocks;
using Web.Models.Blocks.Converter;

namespace Web.Profiles
{
    public class ScenarioProfile : Profile
    {
        public ScenarioProfile()
        {
            CreateMap<Scenario, ScenarioDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => BlockIdProcessor.GetIdDTO(BlockType.Meta, model.Id)))
                .ForMember(dto => dto.Type, opt => opt.MapFrom(model => BlockType.Meta))
                .ForMember(dto => dto.Caption, opt => opt.MapFrom(model => string.IsNullOrEmpty(model.Caption) ? "meta" : model.Caption))
                .ForMember(dto => dto.Scenes, opt => opt.MapFrom(model => model.Details.Scenes));

            CreateMap<ScenarioDto, Scenario>()
                .ForPath(model => model.Details.Scenes, opt => opt.MapFrom(dto => dto.Scenes))
                .AfterMap((dto, model) =>
                {
                    foreach (var scene in model.Details.Scenes)
                    {
                        scene.ScenarioDetails = model.Details;
                    }
                });

            CreateMap<Scene, SceneDto>()
                .ForMember(model => model.UseFromTimeTicks, opt => opt.Ignore())
                .ForMember(model => model.UseToTimeTicks, opt => opt.Ignore())
                .ForMember(model => model.DateToUseTicks, opt => opt.Ignore());

            CreateMap<SceneDto, Scene>()
                .ForMember(model => model.Blocks, opt => opt.Ignore())
                .ForMember(model => model.ScenarioDetails, opt => opt.Ignore());
        }
    }
}
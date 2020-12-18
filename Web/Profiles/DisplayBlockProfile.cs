using AutoMapper;
using DomainObjects.Blocks;
using Web.Models.Blocks;
using Web.Models.Blocks.Converter;

namespace Web.Profiles
{
    public class DisplayBlockProfile : Profile
    {
        public DisplayBlockProfile()
        {
            CreateMap<DisplayBlock, BlockDto>()
                .Include<DateTimeBlock, DateTimeBlockDto>()
                .Include<PictureBlock, PictureBlockDto>()
                .Include<TableBlock, TableBlockDto>()
                .Include<TextBlock, TextBlockDto>()
                .Include<Scenario, ScenarioDto>()
                .ForMember(dto => dto.MetablockFrameId, opt => opt.MapFrom(model => model.SceneId))
                .ForMember(dto => dto.Type, opt => opt.Ignore());

            CreateMap<BlockDto, DisplayBlock>()
                .Include<DateTimeBlockDto, DateTimeBlock>()
                .Include<PictureBlockDto, PictureBlock>()
                .Include<TableBlockDto, TableBlock>()
                .Include<TextBlockDto, TextBlock>()
                .Include<ScenarioDto, Scenario>()
                .ForMember(model => model.Id, opt => opt.MapFrom(dto => BlockIdProcessor.FromDTOId(dto.Id)))
                .ForMember(model => model.SceneId, opt => opt.MapFrom(dto => dto.MetablockFrameId))
                .ForMember(model => model.Scene, opt => opt.Ignore());
        }
    }
}
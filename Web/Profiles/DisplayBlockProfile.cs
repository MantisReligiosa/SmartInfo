using AutoMapper;
using DomainObjects.Blocks;
using Web.Models.Blocks;

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
                .Include<Scenario, ScenarioDto>();

            CreateMap<BlockDto, DisplayBlock>()
                .Include<DateTimeBlockDto, DateTimeBlock>()
                .Include<PictureBlockDto, PictureBlock>()
                .Include<TableBlockDto, TableBlock>()
                .Include<TextBlockDto, TextBlock>()
                .Include<ScenarioDto, Scenario>();
        }
    }
}
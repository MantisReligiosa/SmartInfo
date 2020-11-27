using AutoMapper;
using DomainObjects.Blocks;
using Repository.Entities;
using Repository.Entities.DisplayBlockEntities;

namespace Repository.Profiles
{
    public class DisplayBlockEntityProfile : Profile
    {
        public DisplayBlockEntityProfile()
        {
            CreateMap<DisplayBlockEntity, DisplayBlock>()
                .ForMember(model => model.Scene, opt => opt.MapFrom(entity => entity.Scene))
                .Include<TextBlockEntity, TextBlock>()
                .Include<PictureBlockEntity, PictureBlock>()
                .Include<DateTimeBlockEntity, DateTimeBlock>()
                .Include<TableBlockEntity, TableBlock>()
                .Include<ScenarioEntity, Scenario>();

            CreateMap<DisplayBlock, DisplayBlockEntity>()
                .ForMember(entity => entity.Scene, opt => opt.MapFrom(model => model.Scene))
                .Include<TextBlock, TextBlockEntity>()
                .Include<PictureBlock, PictureBlockEntity>()
                .Include<DateTimeBlock, DateTimeBlockEntity>()
                .Include<TableBlock, TableBlockEntity>()
                .Include<Scenario, ScenarioEntity>();
        }
    }
}

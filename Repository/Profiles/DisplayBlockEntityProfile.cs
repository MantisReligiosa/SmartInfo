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
                .Include<TextBlockEntity, TextBlock>();
        }
    }
}

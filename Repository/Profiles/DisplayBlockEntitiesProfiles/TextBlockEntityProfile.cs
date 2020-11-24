using AutoMapper;
using DomainObjects.Blocks;
using Repository.Entities.DisplayBlockEntities;

namespace Repository.Profiles.DisplayBlockEntitiesProfiles
{
    public class TextBlockEntityProfile : Profile
    {
        public TextBlockEntityProfile()
        {
            CreateMap<TextBlockEntity, TextBlock>()
                .ForMember(entity => entity.Details, opt => opt.MapFrom(e => e.TextBlockDetails));
        }
    }
}

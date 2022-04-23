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
                .ForMember(model => model.Details, opt => opt.MapFrom(entity => entity.TextBlockDetails));

            CreateMap<TextBlock, TextBlockEntity>()
                .ForMember(entity => entity.TextBlockDetails, opt => opt.MapFrom(model => model.Details))
                .AfterMap((model, entity) => entity.TextBlockDetails.TextBlockEntity = entity);
        }
    }
}

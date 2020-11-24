using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class TextBlockDetailsEntityProfile : Profile
    {
        public TextBlockDetailsEntityProfile()
        {
            CreateMap<TextBlockDetailsEntity, TextBlockDetails>();
        }
    }
}

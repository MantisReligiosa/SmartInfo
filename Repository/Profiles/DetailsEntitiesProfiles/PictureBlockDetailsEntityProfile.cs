using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class PictureBlockDetailsEntityProfile : Profile
    {
        public PictureBlockDetailsEntityProfile()
        {
            CreateMap<PictureBlockDetailsEntity, PictureBlockDetails>();

            CreateMap<PictureBlockDetails, PictureBlockDetailsEntity>()
                .ForMember(entity => entity.PictureBlockEntity, opt => opt.Ignore())
                .ForMember(entity => entity.Id, opt => opt.Ignore());
        }
    }
}

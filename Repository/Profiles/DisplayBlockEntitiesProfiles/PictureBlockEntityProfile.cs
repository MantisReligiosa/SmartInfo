using AutoMapper;
using DomainObjects.Blocks;
using Repository.Entities.DisplayBlockEntities;

namespace Repository.Profiles.DisplayBlockEntitiesProfiles
{
    public class PictureBlockEntityProfile : Profile
    {
        public PictureBlockEntityProfile()
        {
            CreateMap<PictureBlockEntity, PictureBlock>()
                .ForMember(model => model.Details, opt => opt.MapFrom(entity => entity.PictureBlockDetails));

            CreateMap<PictureBlock, PictureBlockEntity>()
                .ForMember(entity => entity.PictureBlockDetails, opt => opt.MapFrom(model => model.Details))
                .AfterMap((model,entity)=>entity.PictureBlockDetails.PictureBlockEntity = entity);
        }
    }
}

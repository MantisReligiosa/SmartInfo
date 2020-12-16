using AutoMapper;
using DomainObjects.Blocks;
using Repository.Entities.DisplayBlockEntities;

namespace Repository.Profiles.DisplayBlockEntitiesProfiles
{
    public class DateTimeBlockEntityProfile : Profile
    {
        public DateTimeBlockEntityProfile()
        {
            CreateMap<DateTimeBlockEntity, DateTimeBlock>()
                .ForMember(model => model.Details, opt => opt.MapFrom(entity => entity.DateTimeBlockDetails));

            CreateMap<DateTimeBlock, DateTimeBlockEntity>()
                .ForMember(entity => entity.DateTimeBlockDetails, opt => opt.MapFrom(model => model.Details))
                .AfterMap((model, entity) => entity.DateTimeBlockDetails.DatetimeBlockEntity = entity);
        }
    }
}

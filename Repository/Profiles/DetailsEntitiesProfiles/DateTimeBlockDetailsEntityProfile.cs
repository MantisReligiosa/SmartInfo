using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class DateTimeBlockDetailsEntityProfile : Profile
    {
        public DateTimeBlockDetailsEntityProfile()
        {
            CreateMap<DateTimeBlockDetailsEntity, DateTimeBlockDetails>()
                .ForMember(model => model.Format, opt => opt.MapFrom(entity => entity.DateTimeFormatEntity));

            CreateMap<DateTimeBlockDetails, DateTimeBlockDetailsEntity>()
                .ForMember(entity => entity.DateTimeFormatEntity, opt => opt.Ignore())
                .ForMember(entity => entity.DateTimeFormatId, opt => opt.MapFrom(model => model.Format.Id))
                .ForMember(entity => entity.DatetimeBlockEntity, opt => opt.Ignore())
                .ForMember(entity => entity.Id, opt => opt.Ignore());
        }
    }
}

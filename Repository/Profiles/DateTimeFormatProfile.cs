using AutoMapper;
using DomainObjects;
using Repository.Entities;

namespace Repository.Profiles
{
    public class DateTimeFormatProfile : Profile
    {
        public DateTimeFormatProfile()
        {
            CreateMap<DateTimeFormatEntity, DateTimeFormat>()
                .ForMember(model => model.IsDateFormat, opt => opt.MapFrom(entity => entity.DateFormatFlag == 1));
            CreateMap<DateTimeFormat, DateTimeFormatEntity>()
                .ForMember(entity => entity.DateFormatFlag, opt => opt.MapFrom(model => model.IsDateFormat ? 1 : 0))
                .ForMember(entity => entity.DatetTimeBlockDetailsEntities, opt => opt.Ignore());
        }
    }
}

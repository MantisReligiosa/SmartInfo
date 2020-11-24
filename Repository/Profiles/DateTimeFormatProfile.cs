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
                .ForMember(d => d.IsDateFormat, opt => opt.MapFrom(d => d.DateFormatFlag == 1));
            CreateMap<DateTimeFormat, DateTimeFormatEntity>()
                .ForMember(d => d.DateFormatFlag, opt => opt.MapFrom(d => d.IsDateFormat ? 1 : 0))
                .ForMember(d => d.DatetTimeBlockDetailsEntities, opt => opt.Ignore());
        }
    }
}

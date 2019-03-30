using AutoMapper;
using DomainObjects.Blocks;
using Web.Models.Blocks;

namespace Web.Profiles
{
    public class MetaBlockProfile : Profile
    {
        public MetaBlockProfile()
        {
            CreateMap<MetaBlock, MetaBlockDto>()
                .ForMember(b => b.Type, opt => opt.MapFrom(b => "meta"))
                .ForMember(b => b.Caption, opt => opt.MapFrom(b => string.IsNullOrEmpty(b.Caption) ? "meta" : b.Caption));

            CreateMap<MetaBlockDto, MetaBlock>();
        }
    }
}
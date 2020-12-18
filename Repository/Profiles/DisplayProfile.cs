using AutoMapper;
using DomainObjects;
using Repository.Entities;

namespace Repository.Profiles
{
    public class DisplayProfile : Profile
    {
        public DisplayProfile()
        {
            CreateMap<DisplayEntity, Display>();
            CreateMap<Display, DisplayEntity>();
        }
    }
}

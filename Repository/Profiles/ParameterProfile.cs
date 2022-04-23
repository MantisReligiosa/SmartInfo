using AutoMapper;
using DomainObjects;
using DomainObjects.Parameters;
using Repository.Entities;
using Repository.Entities.ParameterEntities;

namespace Repository.Profiles
{
    public class ParameterProfile : Profile
    {
        public ParameterProfile()
        {
            CreateMap<ParameterEntity, Parameter>()
                .Include<ScreenHeightEntity, ScreenHeight>()
                .Include<ScreenWidthEntity, ScreenWidth>()
                .Include<BackgroundColorEntity, BackgroundColor>();

            CreateMap<Parameter, ParameterEntity>()
                .Include<ScreenHeight, ScreenHeightEntity>()
                .Include<ScreenWidth, ScreenWidthEntity>()
                .Include<BackgroundColor, BackgroundColorEntity>();

            CreateMap<ScreenHeightEntity, ScreenHeight>();
            CreateMap<ScreenWidthEntity, ScreenWidth>();
            CreateMap<BackgroundColorEntity, BackgroundColor>();

            CreateMap<ScreenHeight, ScreenHeightEntity>();
            CreateMap<ScreenWidth, ScreenWidthEntity>();
            CreateMap<BackgroundColor, BackgroundColorEntity>();
        }
    }
}

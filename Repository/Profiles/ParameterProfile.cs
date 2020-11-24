using AutoMapper;
using DomainObjects;
using Repository.Entities;

namespace Repository.Profiles
{
    public class ParameterProfile : Profile
    {
        public ParameterProfile()
        {
            CreateMap<ParameterEntity, Parameter>();
            CreateMap<Parameter, ParameterEntity>();
        }
    }
}

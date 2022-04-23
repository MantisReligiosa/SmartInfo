using AutoMapper;
using DomainObjects;
using Repository.Entities;
using System;

namespace Repository.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, User>()
                .ForMember(model => model.GUID, opt => opt.MapFrom(entity => Guid.Parse(entity.GuidStr)));
            CreateMap<User, UserEntity>()
                .ForMember(entity => entity.GuidStr, opt => opt.MapFrom(model => model.GUID.ToString()));
        }
    }
}

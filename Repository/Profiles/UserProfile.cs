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
                .ForMember(u => u.GUID, opt => opt.MapFrom(u => Guid.Parse(u.GuidStr)));
            CreateMap<User, UserEntity>()
                .ForMember(u => u.GuidStr, opt => opt.MapFrom(u => u.GUID.ToString()));
        }
    }
}

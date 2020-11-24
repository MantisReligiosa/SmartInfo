using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities;
using System;

namespace Repository.Profiles.DisplayBlockEntitiesProfiles
{
    public class SceneProfile : Profile
    {
        public SceneProfile()
        {
            CreateMap<SceneEntity, MetablockFrame>()
                .ForMember(model => model.Blocks, opt => opt.MapFrom(entity => entity.DisplayBlocks))
                .ForMember(model => model.UseInDate, opt => opt.MapFrom(entity => entity.DateToUse))
                .ForMember(model => model.UseInSun, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0000001) != 0))
                .ForMember(model => model.UseInSat, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0000010) != 0))
                .ForMember(model => model.UseInFri, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0000100) != 0))
                .ForMember(model => model.UseInThu, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0001000) != 0))
                .ForMember(model => model.UseInWed, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0010000) != 0))
                .ForMember(model => model.UseInTue, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0100000) != 0))
                .ForMember(model => model.UseInMon, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b1000000) != 0))
                .ForMember(model => model.UseFromTime, opt => opt.MapFrom(entity => entity.UseFromTime == 0 ? (TimeSpan?)null : new TimeSpan(entity.UseFromTime * TimeSpan.TicksPerSecond)))
                .ForMember(model => model.UseToTime, opt => opt.MapFrom(entity => entity.UseToTime == 0 ? (TimeSpan?)null : new TimeSpan(entity.UseToTime * TimeSpan.TicksPerSecond)))
                .ForMember(model => model.DateToUse, opt => opt.MapFrom(entity => entity.DateToUse == 0 ? (DateTime?)null : entity.DateToUse.FromEpoch()));
        }
    }

    public static class DateTimeConvert
    {
        public static int ToEpoch(this DateTime dateTime)
        {
            var timeSpan = dateTime.Subtract(new DateTime(1970, 1, 1));
            return (int)timeSpan.TotalSeconds;
        }

        public static DateTime FromEpoch(this int epoch)
        {
            return new DateTime(1970, 1, 1).AddSeconds(epoch);
        }
    }
}

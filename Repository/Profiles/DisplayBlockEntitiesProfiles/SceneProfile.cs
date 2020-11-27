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
            CreateMap<SceneEntity, Scene>()
                .ForMember(model => model.Blocks, opt => opt.MapFrom(entity => entity.DisplayBlocks))
                .ForMember(model => model.UseInDate, opt => opt.MapFrom(entity => entity.DateToUse))
                .ForMember(model => model.UseInSun, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0000001) != 0))
                .ForMember(model => model.UseInSat, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0000010) != 0))
                .ForMember(model => model.UseInFri, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0000100) != 0))
                .ForMember(model => model.UseInThu, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0001000) != 0))
                .ForMember(model => model.UseInWed, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0010000) != 0))
                .ForMember(model => model.UseInTue, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b0100000) != 0))
                .ForMember(model => model.UseInMon, opt => opt.MapFrom(entity => (entity.UseInDayOfWeekFlags & 0b1000000) != 0))
                .ForMember(model => model.UseFromTime, opt => opt.MapFrom(entity => FromTimestamp(entity.UseFromTime)))
                .ForMember(model => model.UseToTime, opt => opt.MapFrom(entity => FromTimestamp(entity.UseToTime)))
                .ForMember(model => model.DateToUse, opt => opt.MapFrom(entity => FromEpoch(entity.DateToUse)))
                .ForMember(model => model.ScenarioDetails, opt => opt.MapFrom(entity => entity.ScenarioDetailsEntity));

            CreateMap<Scene, SceneEntity>()
                .ForMember(entity => entity.DisplayBlocks, opt => opt.MapFrom(model => model.Blocks))
                .ForMember(entity => entity.UseInDayOfWeekFlags, opt => opt.MapFrom(model =>
                    (model.UseInSun ? 0b0000001 : 0) +
                    (model.UseInSat ? 0b0000010 : 0) +
                    (model.UseInFri ? 0b0000100 : 0) +
                    (model.UseInThu ? 0b0001000 : 0) +
                    (model.UseInWed ? 0b0010000 : 0) +
                    (model.UseInTue ? 0b0100000 : 0) +
                    (model.UseInMon ? 0b1000000 : 0)
                    ))
                .ForMember(entity => entity.UseFromTime, opt => opt.MapFrom(model => ToTimestamp(model.UseFromTime)))
                .ForMember(entity => entity.UseToTime, opt => opt.MapFrom(model => ToTimestamp(model.UseToTime)))
                .ForMember(entity => entity.DateToUse, opt => opt.MapFrom(model => ToEpoch(model.DateToUse)))
                .ForMember(entity => entity.ScenarioDetailsEntity, opt => opt.MapFrom(model => model.ScenarioDetails))
                .ForMember(entity => entity.ScenarioDetailsEntityId, opt => opt.MapFrom(model => model.ScenarioDetails.Id))
                .ForMember(entity => entity.DisplayBlocks, opt => opt.MapFrom(model=>model.Blocks));
        }

        private TimeSpan? FromTimestamp(int timestamp)
        {
            return timestamp == 0 ? (TimeSpan?)null : new TimeSpan(timestamp * TimeSpan.TicksPerSecond);
        }

        private int ToTimestamp(TimeSpan? timeStamp)
        {
            return (int)(timeStamp?.TotalSeconds ?? 0);
        }

        private DateTime? FromEpoch(int epoch)
        {
            return epoch == 0 ? (DateTime?)null : epoch.FromEpoch();
        }

        private int ToEpoch(DateTime? datetime)
        {
            return datetime?.ToEpoch() ?? 0;
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

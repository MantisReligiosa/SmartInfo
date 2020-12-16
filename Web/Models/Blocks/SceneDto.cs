using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Web.Models.Blocks
{
    public class SceneDto
    {
        public int Id { get; set; }
        public List<BlockDto> Blocks { get; set; }
        public int Index { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }

        public bool UseInTimeInterval { get; set; }
        [XmlIgnore]
        public TimeSpan? UseFromTime { get; set; }
        [XmlIgnore]
        public TimeSpan? UseToTime { get; set; }

        public long UseFromTimeTicks
        {
            get => UseFromTime?.Ticks ?? 0;
            set
            {
                UseFromTime = value == 0 ? null : (TimeSpan?)new TimeSpan(value);
            }
        }

        public long UseToTimeTicks
        {
            get => UseToTime?.Ticks ?? 0;
            set
            {
                UseToTime = value == 0 ? null : (TimeSpan?)new TimeSpan(value);
            }
        }
        public bool UseInDayOfWeek { get; set; }
        /// <summary>
        /// Пн
        /// </summary>
        public bool UseInMon { get; set; }
        /// <summary>
        /// Вт
        /// </summary>
        public bool UseInTue { get; set; }
        /// <summary>
        /// Ср
        /// </summary>
        public bool UseInWed { get; set; }
        /// <summary>
        /// Чт
        /// </summary>
        public bool UseInThu { get; set; }
        /// <summary>
        /// Пт
        /// </summary>
        public bool UseInFri { get; set; }
        /// <summary>
        /// Сб
        /// </summary>
        public bool UseInSat { get; set; }
        /// <summary>
        /// Вс
        /// </summary>
        public bool UseInSun { get; set; }
        public bool UseInDate { get; set; }
        [XmlIgnore]
        public DateTime? DateToUse { get; set; }
        public long DateToUseTicks
        {
            get => DateToUse?.Ticks ?? 0;
            set
            {
                DateToUse = value == 0 ? null : (DateTime?)new DateTime(value);
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;

namespace DomainObjects.Blocks.Details
{
    public class Scene : Identity, ICopyable<Scene>
    {
        public ICollection<DisplayBlock> Blocks { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public bool UseInTimeInterval { get; set; }

        public TimeSpan? UseFromTime { get; set; }

        public TimeSpan? UseToTime { get; set; }

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

        public DateTime? DateToUse { get; set; }
        
        public ScenarioDetails ScenarioDetails { get; set; }

        public Scene()
        {
        }

        public Scene(Scene source)
        {
            CopyFrom(source);
        }

        public void CopyFrom(Scene source)
        {
            Index = source.Index;
            Duration = source.Duration;
            Name = source.Name;

            UseInTimeInterval = source.UseInTimeInterval;
            UseFromTime = source.UseFromTime;
            UseToTime = source.UseToTime;

            UseInDayOfWeek = source.UseInDayOfWeek;
            UseInMon = source.UseInMon;
            UseInTue = source.UseInTue;
            UseInWed = source.UseInWed;
            UseInThu = source.UseInThu;
            UseInFri = source.UseInFri;
            UseInSat = source.UseInSat;
            UseInSun = source.UseInSun;

            UseInDate = source.UseInDate;
            DateToUse = source.DateToUse;

            ScenarioDetails = source.ScenarioDetails;

            Blocks = new List<DisplayBlock>();
            foreach (var block in source.Blocks)
            {
                var clonedBlock = block.Clone();
                clonedBlock.SceneId = Id;
                Blocks.Add(clonedBlock);
            }
        }
    }
}

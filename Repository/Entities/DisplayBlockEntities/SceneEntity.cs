using Repository.Entities.DetailsEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("Scenes")]
    public class SceneEntity : Entity
    {
        [Column("Index")]
        public int Index { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Duration")]
        public int Duration { get; set; }

        [Column("UseInTimeInterval")]
        public int UseInTimeInterval { get; set; }

        [Column("UseFromTime")]
        public int UseFromTime { get; set; }

        [Column("UseToTime")]
        public int UseToTime { get; set; }

        [Column("UseInDayOfWeek")]
        public int UseInDayOfWeek { get; set; }

        [Column("UseInDayOfWeekFlags")]
        public int UseInDayOfWeekFlags { get; set; }

        [Column("DateToUse")]
        public int DateToUse { get; set; }

        [Column("ScenarioDetailsId")]
        public int ScenarioDetailsEntityId { get; set; }

        public virtual ICollection<DisplayBlockEntity> DisplayBlocks { get; set; }

        public virtual ScenarioDetailsEntity ScenarioDetailsEntity { get; set; }
       
    }
}

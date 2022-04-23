using Repository.Entities.DetailsEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("DateTimeFormats")]
    public class DateTimeFormatEntity : Entity
    {

        [Column("Denomination")]
        public string Denomination { get; set; }

        [Column("ShowtimeFormat")]
        public string ShowtimeFormat { get; set; }

        [Column("DesigntimeFormat")]
        public string DesigntimeFormat { get; set; }

        [Column("IsDateFormat")]
        public int DateFormatFlag { get; set; }

        public ICollection<DateTimeBlockDetailsEntity> DatetTimeBlockDetailsEntities { get; set; }
    }
}

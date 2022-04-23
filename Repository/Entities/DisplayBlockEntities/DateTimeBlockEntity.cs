using Repository.Entities.DetailsEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DisplayBlockEntities
{
    public class DateTimeBlockEntity : DisplayBlockEntity
    {
        public virtual DateTimeBlockDetailsEntity DateTimeBlockDetails { get; set; }
    }
}

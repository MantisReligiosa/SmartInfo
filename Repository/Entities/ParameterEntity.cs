using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("Parameters")]
    public class ParameterEntity : Entity
    {
        [Column("Value")]
        public string Value { get; set; }
    }
}

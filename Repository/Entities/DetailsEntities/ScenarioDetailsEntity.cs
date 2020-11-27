using Repository.Entities.DisplayBlockEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities.DetailsEntities
{
    [Table("ScenarioDetails")]
    public class ScenarioDetailsEntity : Entity
    {
        public virtual ICollection<SceneEntity> Scenes { get; set; }
        public virtual ScenarioEntity ScenarioEntity { get; set; }
    }
}

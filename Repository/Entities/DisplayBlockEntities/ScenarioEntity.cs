using Repository.Entities.DetailsEntities;

namespace Repository.Entities.DisplayBlockEntities
{
    public class ScenarioEntity : DisplayBlockEntity
    {
        public virtual ScenarioDetailsEntity ScenarioDetails { get; set; }
    }
}

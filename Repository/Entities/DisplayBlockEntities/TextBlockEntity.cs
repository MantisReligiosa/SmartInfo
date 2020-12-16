using Repository.Entities.DetailsEntities;

namespace Repository.Entities.DisplayBlockEntities
{
    public class TextBlockEntity : DisplayBlockEntity
    {
        public virtual TextBlockDetailsEntity TextBlockDetails { get; set; }
    }
}

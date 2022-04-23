using Repository.Entities.DetailsEntities;

namespace Repository.Entities.DisplayBlockEntities
{
    public class TableBlockEntity : DisplayBlockEntity
    {
        public virtual TableBlockDetailsEntity TableBlockDetails { get; set; }
    }
}

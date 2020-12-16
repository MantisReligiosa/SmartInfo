using Repository.Entities.DetailsEntities;

namespace Repository.Entities.DisplayBlockEntities
{
    public class PictureBlockEntity : DisplayBlockEntity
    {
        public virtual PictureBlockDetailsEntity PictureBlockDetails { get; set; }
    }
}

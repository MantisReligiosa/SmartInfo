using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class PictureBlock : DisplayBlock
    {
        public PictureBlock() { }

        public PictureBlock(PictureBlock source) : base(source) { }

        public PictureBlockDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            Details = new PictureBlockDetails(((PictureBlock)source).Details);
        }
    }
}

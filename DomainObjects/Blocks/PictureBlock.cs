using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class PictureBlock : DisplayBlock
    {
        public PictureBlock()
            : base()
        {

        }

        public PictureBlock(PictureBlock source)
            : base(source)
        {

        }

        public PictureBlockDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            var sourceDetails = ((PictureBlock)source).Details;
            if (Details == null)
            {
                Details = new PictureBlockDetails();
            }
            Details.CopyFrom(sourceDetails);
        }

        internal override DisplayBlock Clone()
        {
            return new PictureBlock(this);
        }
    }
}

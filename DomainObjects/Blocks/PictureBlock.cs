using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class PictureBlock : DisplayBlock
    {
        public PictureBlock() { }

        public PictureBlock(PictureBlock source) : base(source)
        {
            Details = new PictureBlockDetails
            {
                Base64Image = source.Details.Base64Image
            };
        }

        public PictureBlockDetails Details { get; set; }
    }
}

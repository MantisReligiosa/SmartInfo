using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class TextBlock : DisplayBlock
    {
        public TextBlock()
            :base()
        {

        }

        public TextBlock(TextBlock source)
            :base(source)
        {

        }

        public TextBlockDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            var sourceDetails = ((TextBlock)source).Details;
            if (Details == null)
            {
                Details = new TextBlockDetails();
            }
            Details.CopyFrom(sourceDetails);
        }

        internal override DisplayBlock Clone()
        {
            return new TextBlock(this);
        }
    }
}

using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class MetaBlock : DisplayBlock
    {
        public MetaBlock()
            : base()
        {

        }

        public MetaBlock(MetaBlock source)
            : base(source)
        {

        }

        public MetaBlockDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            var sourceDetails = ((MetaBlock)source).Details;
            if (Details == null)
            {
                Details = new MetaBlockDetails();
            }
            Details.CopyFrom(sourceDetails);
        }

        internal override DisplayBlock Clone()
        {
            return new MetaBlock(this);
        }
    }
}

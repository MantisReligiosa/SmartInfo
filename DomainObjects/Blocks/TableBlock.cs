using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class TableBlock : DisplayBlock
    {
        public TableBlock()
            : base()
        {

        }

        public TableBlock(TableBlock source)
            :base(source)
        {

        }

        public TableBlockDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            var sourceDetails = ((TableBlock)source).Details;
            if (Details == null)
            {
                Details = new TableBlockDetails();
            }
            Details.CopyFrom(sourceDetails);
        }

        internal override DisplayBlock Clone()
        {
            return new TableBlock(this);
        }
    }
}

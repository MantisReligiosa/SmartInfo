using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class DateTimeBlock : DisplayBlock
    {
        public DateTimeBlock()
            : base()
        {

        }

        public DateTimeBlock(DateTimeBlock source)
            : base(source)
        {

        }

        public DateTimeBlockDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            var sourceDetails = ((DateTimeBlock)source).Details;
            if (Details == null)
            {
                Details = new DateTimeBlockDetails();
            }
            Details.CopyFrom(sourceDetails);
        }
    }
}

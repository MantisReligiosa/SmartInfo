using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks;

public class DateTimeBlock : DisplayBlock
{
    public DateTimeBlock() { }

    public DateTimeBlock(DisplayBlock source)
        : base(source) { }

    public DateTimeBlockDetails Details { get; set; }

    internal override void CopyDetails(DisplayBlock source)
    {
        var sourceDetails = ((DateTimeBlock)source).Details;
        Details.CopyFrom(sourceDetails);
    }

    internal override DisplayBlock Clone()
    {
        return new DateTimeBlock(this);
    }
}
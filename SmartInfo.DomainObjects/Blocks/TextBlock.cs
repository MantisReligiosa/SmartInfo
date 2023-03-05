using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks;

public class TextBlock : DisplayBlock
{
    public TextBlock() { }

    public TextBlock(DisplayBlock source)
        :base(source)
    {

    }

    public TextBlockDetails Details { get; set; }

    internal override void CopyDetails(DisplayBlock source)
    {
        var sourceDetails = ((TextBlock)source).Details;
        Details.CopyFrom(sourceDetails);
    }

    internal override DisplayBlock Clone()
    {
        return new TextBlock(this);
    }
}
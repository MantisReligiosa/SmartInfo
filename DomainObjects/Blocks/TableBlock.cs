using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks;

public class TableBlock : DisplayBlock
{
    public TableBlock() { }

    public TableBlock(TableBlock source)
        :base(source) { }

    public TableBlockDetails Details { get; set; }

    internal override void CopyDetails(DisplayBlock source)
    {
        var sourceDetails = ((TableBlock)source).Details;
        Details.CopyFrom(sourceDetails);
    }

    internal override DisplayBlock Clone()
    {
        return new TableBlock(this);
    }
}
using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks;

public class PictureBlock : DisplayBlock
{
    public PictureBlock() { }

    public PictureBlock(DisplayBlock source)
        : base(source) { }

    public PictureBlockDetails Details { get; set; }

    internal override void CopyDetails(DisplayBlock source)
    {
        var sourceDetails = ((PictureBlock)source).Details;
        Details.CopyFrom(sourceDetails);
    }

    internal override DisplayBlock Clone()
    {
        return new PictureBlock(this);
    }
}
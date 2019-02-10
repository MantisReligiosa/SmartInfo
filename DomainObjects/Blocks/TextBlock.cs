using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class TextBlock : DisplayBlock
    {
        public TextBlock() : base() { }

        public TextBlock(TextBlock source) : base(source) { }

        public TextBlockDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            Details = new TextBlockDetails(((TextBlock)source).Details);
        }
    }
}

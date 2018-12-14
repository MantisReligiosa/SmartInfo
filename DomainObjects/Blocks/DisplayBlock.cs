using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public abstract class DisplayBlock : Identity
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}

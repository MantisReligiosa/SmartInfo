namespace DomainObjects.Blocks
{
    public abstract class DisplayBlock : Identity
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ZIndex { get; set; }
    }

    public enum Align
    {
        Left = 0,
        Center,
        Right
    }
}

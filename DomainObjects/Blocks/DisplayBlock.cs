using DomainObjects.Blocks.Details;
using System;

namespace DomainObjects.Blocks
{
    public abstract class DisplayBlock : Identity
    {
        public DisplayBlock() { }

        public DisplayBlock(DisplayBlock source)
        {
            CopyFrom(source);
        }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ZIndex { get; set; }
        public string Caption { get; set; }
        public Guid? MetablockFrameId { get; set; }
        public MetablockFrame MetablockFrame { get; set; }

        public void CopyFrom(DisplayBlock source)
        {
            Height = source.Height;
            Width = source.Width;
            ZIndex = source.ZIndex;
            Left = source.Left;
            Top = source.Top;
            Caption = source.Caption;
            MetablockFrameId = source.MetablockFrameId;
            CopyDetails(source);
        }

        internal abstract void CopyDetails(DisplayBlock source);
    }

    public enum Align
    {
        Left = 0,
        Center,
        Right
    }
}

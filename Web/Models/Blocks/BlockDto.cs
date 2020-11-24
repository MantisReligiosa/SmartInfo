using System;

namespace Web.Models.Blocks
{
    public class BlockDto
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public int ZIndex { get; set; }
        public string Caption { get; set; }
        public int? MetablockFrameId { get; set; }
    }
}
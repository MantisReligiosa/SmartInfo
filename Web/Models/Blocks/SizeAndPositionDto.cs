using System;

namespace Web.Models.Blocks
{
    public class SizeAndPositionDto
    {
        public Guid Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
    }
}
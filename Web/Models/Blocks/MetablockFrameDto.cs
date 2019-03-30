using System;

namespace Web.Models.Blocks
{
    public class MetablockFrameDto
    {
        public Guid Id { get; set; }
        public BlockDto[] Blocks { get; set; }
        public int Index { get; set; }
        public int Duration { get; set; }
    }
}
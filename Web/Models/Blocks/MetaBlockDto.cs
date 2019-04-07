using System.Collections.Generic;

namespace Web.Models.Blocks
{
    public class MetaBlockDto : BlockDto
    {
        public List<MetablockFrameDto> Frames { get; set; }
    }
}
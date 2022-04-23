using DomainObjects.Blocks.Details;

namespace Web.Models.Blocks
{
    public class PictureBlockDto : BlockDto
    {
        public string Base64Src { get; set; }
        public int ImageMode { get; set; }

        public bool SaveProportions { get; set; }
    }
}
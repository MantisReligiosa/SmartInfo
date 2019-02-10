namespace DomainObjects.Blocks.Details
{
    public class PictureBlockDetails : BlockDetails
    {
        public string Base64Image { get; set; }

        public PictureBlockDetails() { }

        public PictureBlockDetails(PictureBlockDetails source)
        {
            Base64Image = source.Base64Image;
        }
    }
}

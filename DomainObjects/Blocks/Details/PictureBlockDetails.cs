using System;

namespace DomainObjects.Blocks.Details
{
    public class PictureBlockDetails : Identity, ICopyable<PictureBlockDetails>
    {
        public string Base64Image { get; set; }

        public PictureBlockDetails() { }

        public PictureBlockDetails(PictureBlockDetails source)
        {
            CopyFrom(source);
        }

        public void CopyFrom(PictureBlockDetails sourceDetails)
        {
            Base64Image = sourceDetails.Base64Image;
        }
    }
}

namespace DomainObjects.Blocks.Details;

public class PictureBlockDetails : Identity, ICopyable<PictureBlockDetails>
{
    public string Base64Image { get; set; }

    public ImageMode ImageMode { get; set; }

    public bool SaveProportions { get; set; }

    public PictureBlockDetails()
    { }

    public PictureBlockDetails(PictureBlockDetails source)
    {
        CopyFrom(source);
    }

    public void CopyFrom(PictureBlockDetails sourceDetails)
    {
        Base64Image = sourceDetails.Base64Image;
        ImageMode = sourceDetails.ImageMode;
        SaveProportions = sourceDetails.SaveProportions;
    }
}

public enum ImageMode
{
    Crop = 0,
    Zoom = 1
}
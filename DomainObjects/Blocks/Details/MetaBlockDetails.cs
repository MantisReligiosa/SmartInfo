namespace DomainObjects.Blocks.Details
{
    public class MetaBlockDetails : Identity, ICopyable<MetaBlockDetails>
    {
        public MetaBlockDetails() { }

        public MetaBlockDetails(MetaBlockDetails source)
        {
            CopyFrom(source);
        }

        public void CopyFrom(MetaBlockDetails source)
        {
        }
    }
}

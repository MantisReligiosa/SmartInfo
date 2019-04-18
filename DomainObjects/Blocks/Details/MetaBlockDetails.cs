using System.Collections.Generic;

namespace DomainObjects.Blocks.Details
{
    public class MetaBlockDetails : Identity, ICopyable<MetaBlockDetails>
    {
        public ICollection<MetablockFrame> Frames { get; set; }

        public MetaBlockDetails() { }

        public MetaBlockDetails(MetaBlockDetails source)
        {
            CopyFrom(source);
        }

        public void CopyFrom(MetaBlockDetails source)
        {
            Frames = new List<MetablockFrame>();
            foreach(var frame in source.Frames)
            {
                Frames.Add(new MetablockFrame(frame));
            }
        }
    }
}

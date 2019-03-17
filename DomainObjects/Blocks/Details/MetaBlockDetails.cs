using System;

namespace DomainObjects.Blocks.Details
{
    public class MetaBlockDetails : Identity, ICopyable<MetaBlockDetails>
    {
        public void CopyFrom(MetaBlockDetails source)
        {
            throw new NotImplementedException();
        }
    }
}

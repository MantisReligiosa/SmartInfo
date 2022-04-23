using DomainObjects;
using DomainObjects.Blocks;
using System.Collections.Generic;

namespace DataExchange.Requests
{
    public class ApplyChangesRequest : IRequest
    {
        public string Background { get; set; }
        public IEnumerable<DisplayBlock> Blocks { get; set; }
    }
}

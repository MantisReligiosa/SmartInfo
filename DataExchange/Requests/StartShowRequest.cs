using DomainObjects;
using DomainObjects.Blocks;
using System.Collections.Generic;

namespace DataExchange.Requests
{
    public class StartShowRequest : IRequest
    {
        public string Background { get; set; }
        public IEnumerable<DisplayBlock> Blocks { get; set; }
        public ScreenInfo Screens { get; set; }
    }
}

using DomainObjects;
using DomainObjects.Blocks;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IScreenController
    {
        Videopanel GetDatabaseScreenInfo();
        Videopanel GetSystemScreenInfo();
        void SetDatabaseScreenInfo(Videopanel screenInfo);
        TextBlock AddTextBlock();
        IEnumerable<DisplayBlock> GetBlocks();
    }
}

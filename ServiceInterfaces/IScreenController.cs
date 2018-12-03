using DomainObjects;
using DomainObjects.Blocks;

namespace ServiceInterfaces
{
    public interface IScreenController
    {
        Videopanel GetDatabaseScreenInfo();
        Videopanel GetSystemScreenInfo();
        void SetDatabaseScreenInfo(Videopanel screenInfo);
        TextBlock AddTextBlock();
    }
}

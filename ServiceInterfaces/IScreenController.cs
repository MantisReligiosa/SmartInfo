using DomainObjects;

namespace ServiceInterfaces
{
    public interface IScreenController
    {
        Videopanel GetDatabaseScreenInfo();
        Videopanel GetSystemScreenInfo();
        void SetDatabaseScreenInfo(Videopanel screenInfo);
    }
}

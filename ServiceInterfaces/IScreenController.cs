using DomainObjects;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IScreenController
    {
        ScreenInfo GetDatabaseScreenInfo();
        ScreenInfo GetSystemScreenInfo();
        void SetDatabaseScreenInfo(ScreenInfo screenInfo);
    }
}

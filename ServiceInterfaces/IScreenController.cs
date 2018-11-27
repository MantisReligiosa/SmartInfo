using DomainObjects;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IScreenController
    {
        Videopanel GetDatabaseScreenInfo();
        Videopanel GetSystemScreenInfo();
        void SetDatabaseScreenInfo(Videopanel screenInfo);
    }
}

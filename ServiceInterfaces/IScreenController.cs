using DomainObjects;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IScreenController
    {
        Task<ScreenInfo> GetDatabaseScreenInfoAsync();
        Task<ScreenInfo> GetSystemScreenInfoAsync();
        void SetDatabaseScreenInfoAsync(ScreenInfo screenInfo);
    }
}

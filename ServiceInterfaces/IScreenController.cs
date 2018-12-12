using DomainObjects;

namespace ServiceInterfaces
{
    public interface IScreenController
    {
        Videopanel GetDatabaseScreenInfo();
        Videopanel GetSystemScreenInfo();
        void SetDatabaseScreenInfo(Videopanel screenInfo);

        void SetBackground(string color);
        string GetBackground();
    }
}

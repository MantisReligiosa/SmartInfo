using System.Collections.Generic;
using DomainObjects;

namespace ServiceInterfaces
{
    public interface ISystemController
    {
        Videopanel GetDatabaseScreenInfo();
        Videopanel GetSystemScreenInfo();
        void SetDatabaseScreenInfo(Videopanel screenInfo);

        void SetBackground(string color);
        string GetBackground();
        IEnumerable<int> GetFontSizes();
        IEnumerable<string> GetFonts();
        IEnumerable<double> GetFontHeightIndex();
    }
}

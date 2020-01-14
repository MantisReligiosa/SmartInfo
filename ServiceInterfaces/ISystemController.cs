using System.Collections.Generic;
using DomainObjects;

namespace ServiceInterfaces
{
    public interface ISystemController
    {
        ScreenInfo GetDatabaseScreenInfo();
        ScreenInfo GetSystemScreenInfo();
        void SetDatabaseScreenInfo(ScreenInfo screenInfo);

        IEnumerable<int> GetFontSizes();
        IEnumerable<string> GetFonts();
        IEnumerable<double> GetFontHeightIndex();
        string GetVersion();
        IEnumerable<DateTimeFormat> GetDatetimeFormats();
    }
}

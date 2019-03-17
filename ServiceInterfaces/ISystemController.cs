using System.Collections.Generic;
using DomainObjects;

namespace ServiceInterfaces
{
    public interface ISystemController
    {
        Videopanel GetDatabaseScreenInfo();
        Videopanel GetSystemScreenInfo();
        void SetDatabaseScreenInfo(Videopanel screenInfo);

        IEnumerable<int> GetFontSizes();
        IEnumerable<string> GetFonts();
        IEnumerable<double> GetFontHeightIndex();
        string GetVersion();
        IEnumerable<DateTimeFormat> GetDatetimeFormats();
    }
}

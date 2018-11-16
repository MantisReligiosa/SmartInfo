using System.Collections.Generic;

namespace Web.Models
{
    public class FontInfo
    {
        public IEnumerable<Font> Fonts { get; set; }
        public IEnumerable<int> FonSizes { get; set; }
    }
}
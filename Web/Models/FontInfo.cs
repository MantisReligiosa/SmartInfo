using System.Collections.Generic;

namespace Web.Models
{
    public class FontInfo
    {
        public IEnumerable<string> Fonts { get; set; }
        public IEnumerable<int> Sizes { get; set; }
        public IEnumerable<double> Indexes { get; set; }
    }
}
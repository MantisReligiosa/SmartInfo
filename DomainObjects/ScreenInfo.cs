using System.Collections.Generic;

namespace DomainObjects
{
    public class ScreenInfo
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public ScreenDetail[] ScreenDetails { get; set; }
    }
}

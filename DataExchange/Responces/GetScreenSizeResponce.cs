using DataExchange.DTO;
using System.Collections.Generic;

namespace DataExchange.Responces
{
    public class GetScreenSizeResponce : IResponce
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public IEnumerable<ScreenSize> Screens { get; set; }
    }
}

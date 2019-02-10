using System.Collections.Generic;

namespace DataExchange.Responces
{
    public class GetFontsResponce : IResponce
    {
        public IEnumerable<string> Fonts { get; set; }
    }
}

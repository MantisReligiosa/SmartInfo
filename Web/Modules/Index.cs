using Nancy;
using Web.Models;

namespace Web.Modules
{
    public class Index : NancyModule
    {
        public Index()
        {
            Get["/"] = parameters => View["Index.cshtml", new UserModel()];
        }
    }
}
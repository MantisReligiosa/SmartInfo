using Nancy;

namespace Web.Modules
{
    public class Index : NancyModule
    {
        public Index()
        {
            Get["/"] = parameters => "Hello World";
        }
    }
}
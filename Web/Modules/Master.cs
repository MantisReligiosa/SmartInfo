using Nancy;

namespace Web.Modules
{
    public class Master : NancyModule
    {
        public Master()
        {
            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];
        }
    }
}
using Nancy;
using Nancy.Security;

namespace Web.Modules
{
    public class ViewModule : NancyModule
    {
        public ViewModule()
        {
            this.RequiresAuthentication();
            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];
            Get["/"] = parameters => View["Views/Home/Master.cshtml"];
        }
    }
}
using Nancy;

namespace Web.Modules
{
    public class Master : NancyModule
    {
        public Master()
        {
            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];

            Post["/master/fonts"] = parameters =>
            {
                return Response.AsJson(new[] { new{ id = 1, text = "Font1" }, new { id = 2, text = "Font2" } });
            };

            Post["/master/fontSizes"] = parameters =>
            {
                return Response.AsJson(new[] { new { id = 1, text = "8" }, new { id = 2, text = "10" } });
            };
        }
    }
}
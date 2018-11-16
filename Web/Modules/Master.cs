using Nancy;
using Web.Models;

namespace Web.Modules
{
    public class Master : NancyModule
    {
        public Master()
        {
            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];

            Post["/master/fonts"] = parameters =>
            {
                return Response.AsJson(new FontInfo
                {
                    Fonts = new Font[] { new Font { Id = 1, Name = "Font1" }, new Font { Id = 2, Name = "Font2" } },
                    FonSizes = new[] { 8, 10 }
                });
            };
        }
    }
}
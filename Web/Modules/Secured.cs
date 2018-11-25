﻿using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using ServiceInterfaces;
using Web.Models;

namespace Web.Modules
{
    public class Secured : NancyModule
    {
        public Secured(
            IScreenController screenController
            )
        {
            this.RequiresAuthentication();
            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];
            Get["/"] = parameters => View["Views/Home/Master.cshtml"];
            Post["/api/fonts"] = parameters =>
                {
                    return Response.AsJson(new FontInfo
                    {
                        Fonts = new Font[] { new Font { Id = 1, Name = "Font1" }, new Font { Id = 2, Name = "Font2" } },
                        FonSizes = new[] { 8, 10 }
                    });
                };
            Post["/api/screenResolution", true] = async (x, context) =>
                {
                    var data = this.Bind<ScreenResolutionRequest>();
                    if (!data.RefreshData)
                    {
                        var screenInfo = await screenController.GetDatabaseScreenInfoAsync();
                        return Response.AsJson(screenInfo);
                    }
                    else
                    {
                        var screenInfo = await screenController.GetSystemScreenInfoAsync();
                        screenController.SetDatabaseScreenInfoAsync(screenInfo);
                        return Response.AsJson(screenInfo);
                    }
                };
        }
    }
}
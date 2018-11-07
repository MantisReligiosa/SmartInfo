using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(Web.Startup))]

namespace Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var webApiConfiguration = new HttpConfiguration();
            //webApiConfiguration.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{controller}/{id}"
            //    //,
            //    //defaults: new { id = RouteParameter.Optional, controller = "values" }
            //    );

            app.UseNinjectMiddleware(CreateKernel);
            //app.UseNinjectWebApi(webApiConfiguration);
            app.UseNancy();
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}

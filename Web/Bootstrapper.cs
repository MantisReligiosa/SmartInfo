using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
        }
    }
}
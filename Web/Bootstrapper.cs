using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bundle;
using Nancy.Bundle.Settings;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Web.Bundles;

namespace Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            StaticConfiguration.EnableRequestTracing = true;
            var config = container.Resolve<IConfigSettings>();

            container.AttachNancyBundle<MyBundleConfig>(cfg =>
            {
                cfg.AddContentGroup(new MyCustomCssBundle());
                cfg.AddContentGroup(new MyJsBundle());
            });

        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("images", @"images"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
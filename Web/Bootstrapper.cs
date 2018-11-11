using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bundle;
using Nancy.Bundle.Settings;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Web.Bundles;
using Web.Bundles.Web.Bundles;

namespace Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var config = container.Resolve<IConfigSettings>();

            container.AttachNancyBundle<BundleConfig>(cfg =>
            {
                cfg.AddContentGroup(new CssBundles());
                cfg.AddContentGroup(new JsBundles());
            });
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(
               StaticContentConventionBuilder.AddDirectory("Images", @"Images"));
        }
    }
}
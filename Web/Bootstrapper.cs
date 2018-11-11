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

            container.AttachNancyBundle<MyBundleConfig>(cfg =>
            {
                cfg.AddContentGroup(new CssBundles());
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
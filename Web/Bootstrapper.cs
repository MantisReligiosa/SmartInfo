using DataExchange;
using DataExchange.Requests;
using DataExchange.Responces;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bundle;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Repository;
using ServiceInterfaces;
using Services;
using Web.Bundles;
using Web.Bundles.Web.Bundles;

namespace Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            //var config = container.Resolve<IConfigSettings>();

            container.AttachNancyBundle<BundleConfig>(cfg =>
            {
                cfg.AddContentGroup(new CssBundles());
                cfg.AddContentGroup(new VendorJsBundles());
                cfg.AddContentGroup(new AppJsBundles());
                cfg.AddContentGroup(new MasterJsBundle());
            });

            container.Register<IConfiguration, Configuration>();
            container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.Register<ICryptoProvider, CryptoProvider>();
            container.Register<IAccountController, AccountController>();
            container.Register<IScreenController, ScreenController>();

            CustomStatusCode.AddCode(404);

            pipelines.OnError += (ctx, ex) =>
            {
                return null;
            };

            var config = container.Resolve<IConfiguration>();
            if (config.BrokerType.Equals("Fake", System.StringComparison.InvariantCultureIgnoreCase))
            {
                var broker = Broker.GetBroker();
                broker.RegisterHandler<GetScreenSizeRequest>((request) =>
                {
                    var responce = new GetScreenSizeResponce
                    {
                        Height = 1050 * 2,
                        Width = 1680 * 2
                    };
                    return responce;
                });
            }
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            var formsAuthConfiguration = new FormsAuthenticationConfiguration
            {
                RedirectUrl = "~/login",
                UserMapper = container.Resolve<IUserMapper>(),
            };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(
               StaticContentConventionBuilder.AddDirectory("Images", @"Images"));
        }
    }
}
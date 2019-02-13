using DataExchange;
using DataExchange.DTO;
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
            container.Register<ISystemController, SystemController>();
            container.Register<IBlockController, BlockController>();
            container.Register<IOperationController, OperationController>();

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
                    var height = 480;
                    var width = 640;
                    var responce = new GetScreenSizeResponce
                    {
                        Height = height * 2,
                        Width = width * 2,
                        Screens = new ScreenSize[]
                        {
                            new ScreenSize
                            {
                                Height = height,
                                Width = width,
                                Top = 0,
                                Left = 0
                            },
                            new ScreenSize
                            {
                                Height = height,
                                Width = width,
                                Top = 0,
                                Left = width
                            },
                            new ScreenSize
                            {
                                Height = height,
                                Width = width,
                                Top = height,
                                Left = 0
                            },
                            new ScreenSize
                            {
                                Height = height,
                                Width = width,
                                Top = height,
                                Left = width
                            }
                        }
                    };
                    return responce;
                });
                broker.RegisterHandler<GetFontsRequest>(request =>
                {
                    return new GetFontsResponce
                    {
                        Fonts = new string[]
                        {
                            "Arial",
                            "Calibri",
                            "Comic Sans MS",
                            "Consolas",
                            "Courier New",
                            "Tahoma",
                            "Times New Roman"
                        }
                    };
                });
                broker.RegisterHandler<StartShowRequest>(reqest =>
                {
                    return null;
                });
                broker.RegisterHandler<GetVersionRequest>(request =>
                {
                    return new GetVersionResponce
                    {
                        Major = 12,
                        Minor = 34,
                        Build = 56
                    };
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
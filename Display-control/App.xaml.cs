using DataExchange;
using DataExchange.Requests;
using DataExchange.Responces;
using Nancy.Hosting.Self;
using System;
using System.Windows;
using Web;

namespace Display_control
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var broker = Broker.GetBroker();
            broker.RegisterHandler<GetScreenSizeRequest>((request) =>
            {
                var responce = new GetScreenSizeResponce
                {
                    Height = (int)SystemParameters.VirtualScreenHeight,
                    Width = (int)SystemParameters.VirtualScreenWidth
                };
                return responce;
            });

            var hostConfiguration = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            var uri = new Uri("http://localhost:1234");
            var bootstrapper = new Bootstrapper();
            var nancyHost = new NancyHost(bootstrapper, hostConfiguration, uri);

            //var nancyHost = new NancyHost(new Uri("http://localhost:1234"), new Bootstrapper());
            nancyHost.Start();
        }
    }
}

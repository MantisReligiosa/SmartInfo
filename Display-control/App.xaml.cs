using DataExchange;
using DataExchange.DTO;
using DataExchange.Requests;
using DataExchange.Responces;
using Nancy.Hosting.Self;
using System;
using System.Linq;
using System.Windows;
using Web;

namespace Display_control
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _window;
        private MainWindowViewModel _viewModel;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var broker = Broker.GetBroker();
            broker.RegisterHandler<GetScreenSizeRequest>(request =>
            {
                var screens = System.Windows.Forms.Screen.AllScreens;
                var responce = new GetScreenSizeResponce
                {
                    Height = (int)SystemParameters.VirtualScreenHeight,
                    Width = (int)SystemParameters.VirtualScreenWidth,
                    Screens = screens.Select(s => new ScreenSize
                    {
                        Left = s.Bounds.X,
                        Top = s.Bounds.Y,
                        Width = s.Bounds.Width,
                        Height = s.Bounds.Height
                    })
                };
                return responce;
            });
            broker.RegisterHandler<GetFontsRequest>(request =>
            {
                return new GetFontsResponce
                {
                    Fonts = System.Drawing.FontFamily.Families.Select(f => f.Name)
                };
            });
            broker.RegisterHandler<StartShowRequest>(request =>
            {
                var requestData = request as StartShowRequest;
                _viewModel.Height = requestData.Screens.Height;
                _viewModel.Width = requestData.Screens.Width;
                return null;
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
            _viewModel = new MainWindowViewModel
            {
                Height = 100,
                Width = 100
            };
            _window = new MainWindow(_viewModel);
            _window.Show();
        }
    }
}

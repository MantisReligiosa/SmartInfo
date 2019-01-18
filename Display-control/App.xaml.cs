using DataExchange;
using DataExchange.DTO;
using DataExchange.Requests;
using DataExchange.Responces;
using Nancy.Hosting.Self;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Web;
using Media = System.Windows.Media;

namespace Display_control
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _window;
        private MainWindowViewModel _viewModel;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = Display_control.Properties.Resources.Logo,
                Visible = true,
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
            };
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, args) => Current.Shutdown();
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
                Dispatcher.Invoke(() =>
                {
                    _window.Height = requestData.Screens.Displays.Max(d => d.Top + d.Height) - requestData.Screens.Displays.Min(d => d.Top);
                    _window.Width = requestData.Screens.Displays.Max(d => d.Left + d.Width) - requestData.Screens.Displays.Min(d => d.Left);
                    _window.Left = requestData.Screens.Displays.Min(d => d.Left);
                    _window.Top = requestData.Screens.Displays.Min(d => d.Top);
                    _window.Visibility = Visibility.Visible;
                    var bc = new Media.BrushConverter();
                    string colorHex;
                    var grid = _window.Content as System.Windows.Controls.Grid;
                    if (TryToParseRGB(requestData.Background, out colorHex))
                    {
                        grid.Background = (Media.Brush)(bc.ConvertFrom(colorHex));
                    }
                    else if (TryToParseRGBA(requestData.Background, out colorHex))
                    {
                        grid.Background = (Media.Brush)(bc.ConvertFrom(colorHex));
                    }
                    _window.Show();
                });
                _viewModel.Blocks = requestData.Blocks;

                return null;
            });
            broker.RegisterHandler<StopShowRequest>(request =>
            {
                Dispatcher.Invoke(() =>
                {
                    _window.Visibility = Visibility.Hidden;
                });
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
                WindowState = WindowState.Normal
            };
            _window = new MainWindow(_viewModel)
            {
                Visibility = Visibility.Hidden
            };
            _notifyIcon.ShowBalloonTip(1, "Display-control", "Сервер запущен и готов к работе", System.Windows.Forms.ToolTipIcon.Info);
        }

        private bool TryToParseRGB(string colorString, out string colorHexString)
        {
            var regular = @"^rgb\(\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(\d{1,3})\)$";
            colorHexString = string.Empty;
            if (Regex.IsMatch(colorString, regular))
            {
                var t = Regex.Matches(colorString, regular);
                var group = t[0].Groups;
                var red = Convert.ToByte(group[1].Value);
                var green = Convert.ToByte(group[2].Value);
                var blue = Convert.ToByte(group[3].Value);
                colorHexString = $"#{red:x}{green:x}{blue:x}";
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool TryToParseRGBA(string colorString, out string colorHexString)
        {
            var regular = @"^rgba\(\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(0\.\d{1,2})\)$";
            colorHexString = string.Empty;
            if (Regex.IsMatch(colorString, regular))
            {
                var t = Regex.Matches(colorString, regular);
                var group = t[0].Groups;
                var red = Convert.ToByte(group[1].Value);
                var green = Convert.ToByte(group[2].Value);
                var blue = Convert.ToByte(group[3].Value);
                var opacity = Convert.ToDouble(group[4].Value.Replace(".", ","));
                var opacityByte = Convert.ToByte(Math.Truncate(opacity * 0xff));
                colorHexString = $"#{opacityByte:x2}{red:x2}{green:x2}{blue:x2}";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

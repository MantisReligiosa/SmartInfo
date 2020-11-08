using Common.Activation;
using Common.Compression;
using Constants;
using DataExchange;
using DataExchange.DTO;
using DataExchange.Requests;
using DataExchange.Responces;
using DomainObjects.Blocks;
using Helpers;
using Nancy.Hosting.Self;
using SmartInfo.Blocks;
using SmartInfo.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Web;
using Media = System.Windows.Media;

namespace SmartInfo
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Window _window;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private IActivationManager _activationManager;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _activationManager = new ActivationManager(Activation.Key, Activation.IV, new Compressor(), new ActivationFile(), new HardwareInfoProvider());
            CheckActivation();
            CreateNotifyIcon();

            try
            {
                RegisterHandlers();
                StartNancy();

                _window = new Window()
                {
                    Visibility = Visibility.Hidden,
                    ShowInTaskbar = false,
                    WindowStyle = WindowStyle.None,
                    Cursor = Cursors.None,
                    ResizeMode = ResizeMode.NoResize,
#if !DEBUG
                    Topmost = true
#endif
                };
                _notifyIcon.ShowBalloonTip(1, "SmartInfo", "Сервер запущен и готов к работе", System.Windows.Forms.ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                _notifyIcon.ShowBalloonTip(1, "SmartInfo", ex.GetInnerException().Message, System.Windows.Forms.ToolTipIcon.Error);
            }
        }

        private static void StartNancy()
        {
            var hostConfiguration = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            var port = Settings.Default.ServerPort;
            var uri = new Uri($"http://localhost:{port}");
            var bootstrapper = new Bootstrapper();
            var nancyHost = new NancyHost(bootstrapper, hostConfiguration, uri);
            nancyHost.Start();
        }

        public void CheckActivation()
        {
            var expectedRequestCode = _activationManager.GetRequestCode();
            if (string.IsNullOrEmpty(_activationManager.ActualLicenseInfo.RequestCode)
                || _activationManager.ActualLicenseInfo.RequestCode != expectedRequestCode)
            {
                ActivationRequired();
            }
            else if (_activationManager.ActualLicenseInfo.ExpirationDate < DateTime.Now)
            {
                TrialExpired();
            }
        }

        private void TrialExpired()
        {
            if (MessageBox.Show("Срок активации истек.\r\n" +
                "Необходимо активировать приложение\r\n" +
                "Перейти к активации?", "Требуется активация", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.No)
            {
                Environment.Exit(0);
            }
            ProceedActivation();
        }

        private void ActivationRequired()
        {
            if (MessageBox.Show("Приложение не активировано.\r\n" +
                "Без активации работа приложения невозможна\r\n" +
                "Перейти к активации?", "Требуется активация", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.No)
            {
                Environment.Exit(0);
            }
            ProceedActivation();
        }

        private void ProceedActivation()
        {
            var activationViewModel = new ActivationViewModel(_activationManager);
            var activationWindow = new ActivationWindow(activationViewModel);
            activationViewModel.GenerateRequestCode();
            if (activationWindow.ShowDialog() == false)
                Environment.Exit(0);
        }

        private void RegisterHandlers()
        {
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
                    _window.WindowState = WindowState.Maximized;
                    ApplyChanges(requestData.Background, requestData.Blocks);

                    _window.Show();
                });
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
            broker.RegisterHandler<GetVersionRequest>(request =>
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return new GetVersionResponce
                {
                    Major = version.Major,
                    Minor = version.Minor,
                    Build = version.Build
                };
            });
            broker.RegisterHandler<ApplyChangesRequest>(request =>
            {
                var requestData = request as ApplyChangesRequest;
                Dispatcher.Invoke(() =>
                {
                    ApplyChanges(requestData.Background, requestData.Blocks);
                });
                return null;
            });
        }

        private void ApplyChanges(string background, IEnumerable<DisplayBlock> blocks)
        {
            var bc = new Media.BrushConverter();
            var border = new Border();
            if (ColorConverter.TryToParseRGB(background, out string colorHex))
            {
                border.Background = (Media.Brush)bc.ConvertFrom(colorHex);
            }
            var canvas = new Canvas();
            var blockBuilder = new BlockBuilder();
            foreach (var block in blocks)
            {
                var element = blockBuilder.BuildElement(block);
                if (element != null)
                {
                    canvas.Children.Add(element);
                }
            }
            border.Child = canvas;
            _window.Content = border;
        }

        private void CreateNotifyIcon()
        {
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = SmartInfo.Properties.Resources.Logo,
                Visible = true,
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
            };
            _notifyIcon.ContextMenuStrip.Items.Add("Выход").Click += (s, args) => Current.Shutdown();
        }
    }
}

using DataExchange;
using DataExchange.DTO;
using DataExchange.Requests;
using DataExchange.Responces;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Web;
using DisplayBlock = DomainObjects.Blocks;
using Media = System.Windows.Media;

namespace Display_control
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Window _window;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = Display_control.Properties.Resources.Logo,
                Visible = true,
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
            };
            notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, args) => Current.Shutdown();
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
                    var bc = new Media.BrushConverter();
                    var border = new Border();
                    if (TryToParseRGB(requestData.Background, out string colorHex))
                    {
                        border.Background = (Media.Brush)bc.ConvertFrom(colorHex);
                    }
                    var canvas = new Canvas();

                    foreach (var block in requestData.Blocks)
                    {
                        if (block is DisplayBlock.TextBlock textBlock)
                        {
                            canvas.Children.Add(BuildTextBlock(textBlock));
                        }
                        else if (block is DisplayBlock.PictureBlock pictureBlock)
                        {
                            canvas.Children.Add(BuildImage(pictureBlock));
                        }
                        else if (block is DisplayBlock.TableBlock tableBlock)
                        {
                            canvas.Children.Add(BuildTable(tableBlock));
                        }
                    }

                    border.Child = canvas;
                    _window.Content = border;
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

            var hostConfiguration = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            var uri = new Uri("http://localhost:1234");
            var bootstrapper = new Bootstrapper();
            var nancyHost = new NancyHost(bootstrapper, hostConfiguration, uri);
            nancyHost.Start();
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
            notifyIcon.ShowBalloonTip(1, "Display-control", "Сервер запущен и готов к работе", System.Windows.Forms.ToolTipIcon.Info);
        }

        private DataGrid BuildTable(DisplayBlock.TableBlock tableBlock)
        {
            var bc = new Media.BrushConverter();
            string colorHex;
            var headerStyle = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));
            if (TryToParseRGB(tableBlock.Details.HeaderDetails.TextColor, out colorHex))
            {
                headerStyle.Setters.Add(new Setter(Control.ForegroundProperty, (Media.Brush)bc.ConvertFrom(colorHex)));
            }
            if (TryToParseRGB(tableBlock.Details.HeaderDetails.BackColor, out colorHex))
            {
                headerStyle.Setters.Add(new Setter(Control.BackgroundProperty, (Media.Brush)bc.ConvertFrom(colorHex)));
            }
            headerStyle.Setters.Add(new Setter(Control.FontSizeProperty, (double)tableBlock.Details.FontSize));
            headerStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new Media.FontFamily(tableBlock.Details.FontName)));
            headerStyle.Setters.Add(new Setter(Control.FontWeightProperty, tableBlock.Details.HeaderDetails.Bold ? FontWeights.Bold : FontWeights.Normal));
            headerStyle.Setters.Add(new Setter(Control.FontStyleProperty, tableBlock.Details.HeaderDetails.Italic ? FontStyles.Italic : FontStyles.Normal));
            headerStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty,
                    tableBlock.Details.HeaderDetails.Align == DisplayBlock.Align.Left ? HorizontalAlignment.Left :
                    tableBlock.Details.HeaderDetails.Align == DisplayBlock.Align.Center ? HorizontalAlignment.Center :
                    tableBlock.Details.HeaderDetails.Align == DisplayBlock.Align.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left));

            var rowStyle = new Style(typeof(DataGridRow));
            var oddTrigger = new Trigger()
            {
                Property = DataGridRow.AlternationIndexProperty,
                Value = 1
            };
            var evenTrigger = new Trigger()
            {
                Property = DataGridRow.AlternationIndexProperty,
                Value = 0
            };
            rowStyle.Triggers.Add(oddTrigger);
            rowStyle.Triggers.Add(evenTrigger);

            if (TryToParseRGB(tableBlock.Details.OddRowDetails.TextColor, out colorHex))
            {
                oddTrigger.Setters.Add(new Setter(Control.ForegroundProperty, (Media.Brush)bc.ConvertFrom(colorHex)));
            }
            if (TryToParseRGB(tableBlock.Details.OddRowDetails.BackColor, out colorHex))
            {
                oddTrigger.Setters.Add(new Setter(Control.BackgroundProperty, (Media.Brush)bc.ConvertFrom(colorHex)));
            }
            oddTrigger.Setters.Add(new Setter(Control.FontSizeProperty, (double)tableBlock.Details.FontSize));
            oddTrigger.Setters.Add(new Setter(Control.FontFamilyProperty, new Media.FontFamily(tableBlock.Details.FontName)));
            oddTrigger.Setters.Add(new Setter(Control.FontWeightProperty, tableBlock.Details.OddRowDetails.Bold ? FontWeights.Bold : FontWeights.Normal));
            oddTrigger.Setters.Add(new Setter(Control.FontStyleProperty, tableBlock.Details.OddRowDetails.Italic ? FontStyles.Italic : FontStyles.Normal));
            oddTrigger.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty,
                    tableBlock.Details.OddRowDetails.Align == DisplayBlock.Align.Left ? HorizontalAlignment.Left :
                    tableBlock.Details.OddRowDetails.Align == DisplayBlock.Align.Center ? HorizontalAlignment.Center :
                    tableBlock.Details.OddRowDetails.Align == DisplayBlock.Align.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left));

            if (TryToParseRGB(tableBlock.Details.EvenRowDetails.TextColor, out colorHex))
            {
                evenTrigger.Setters.Add(new Setter(Control.ForegroundProperty, (Media.Brush)bc.ConvertFrom(colorHex)));
            }
            if (TryToParseRGB(tableBlock.Details.EvenRowDetails.BackColor, out colorHex))
            {
                evenTrigger.Setters.Add(new Setter(Control.BackgroundProperty, (Media.Brush)bc.ConvertFrom(colorHex)));
            }
            evenTrigger.Setters.Add(new Setter(Control.FontSizeProperty, (double)tableBlock.Details.FontSize));
            evenTrigger.Setters.Add(new Setter(Control.FontFamilyProperty, new Media.FontFamily(tableBlock.Details.FontName)));
            evenTrigger.Setters.Add(new Setter(Control.FontWeightProperty, tableBlock.Details.EvenRowDetails.Bold ? FontWeights.Bold : FontWeights.Normal));
            evenTrigger.Setters.Add(new Setter(Control.FontStyleProperty, tableBlock.Details.EvenRowDetails.Italic ? FontStyles.Italic : FontStyles.Normal));
            evenTrigger.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty,
                    tableBlock.Details.EvenRowDetails.Align == DisplayBlock.Align.Left ? HorizontalAlignment.Left :
                    tableBlock.Details.EvenRowDetails.Align == DisplayBlock.Align.Center ? HorizontalAlignment.Center :
                    tableBlock.Details.EvenRowDetails.Align == DisplayBlock.Align.Right ? HorizontalAlignment.Right : HorizontalAlignment.Left));

            var dataGrid = new DataGrid
            {
                HeadersVisibility = DataGridHeadersVisibility.Column,
                Height = tableBlock.Height,
                Width = tableBlock.Width,
                BorderThickness = new Thickness(0),
                GridLinesVisibility = DataGridGridLinesVisibility.None,
                AlternationCount = 2,
                ColumnHeaderStyle = headerStyle,
                RowStyle = rowStyle
            };
            var columnCount = tableBlock.Details.Cells.Max(c => c.Column) + 1;
            var rows = tableBlock.Details.Cells.GroupBy(c => c.Row).OrderBy(r => r.Key);
            var header = rows.FirstOrDefault().OrderBy(c => c.Column).ToArray();
            for (int i = 0; i < columnCount; i++)
            {
                var name = $"Column{i}";
                dataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = header[i].Value,
                    Binding = new Binding(name),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                });
            }
            foreach (var row in rows.Skip(1))
            {
                dynamic tableRow = new ExpandoObject();
                var i = 0;
                foreach (var cell in row.OrderBy(c => c.Column))
                {
                    var name = $"Column{i}";
                    ((IDictionary<string, object>)tableRow)[name] = cell.Value;
                    i++;
                }
                dataGrid.Items.Add(tableRow);
            }


            Canvas.SetTop(dataGrid, tableBlock.Top);
            Canvas.SetLeft(dataGrid, tableBlock.Left);
            Panel.SetZIndex(dataGrid, tableBlock.ZIndex);

            return dataGrid;
        }

        private static Image BuildImage(DisplayBlock.PictureBlock pictureBlock)
        {
            var bitmap = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(pictureBlock.Details.Base64Image)))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // here
                bitmap.StreamSource = ms;
                bitmap.EndInit();
            }

            var _image = new Image
            {
                Height = pictureBlock.Height,
                Width = pictureBlock.Width,
                Source = bitmap,
                Stretch = Media.Stretch.None
            };

            Canvas.SetTop(_image, pictureBlock.Top);
            Canvas.SetLeft(_image, pictureBlock.Left);
            Panel.SetZIndex(_image, pictureBlock.ZIndex);
            return _image;
        }

        private TextBlock BuildTextBlock(DisplayBlock.TextBlock textBlock)
        {
            var bc = new Media.BrushConverter();
            string colorHex;
            var label = new TextBlock
            {
                Height = textBlock.Height,
                Width = textBlock.Width,
                Text = textBlock.Details.Text,
                FontSize = textBlock.Details.FontSize,
                FontFamily = new Media.FontFamily(textBlock.Details.FontName),
                FontWeight = textBlock.Details.Bold ? FontWeights.Bold : FontWeights.Normal,
                FontStyle = textBlock.Details.Italic ? FontStyles.Italic : FontStyles.Normal,
                LineHeight = textBlock.Details.FontSize * textBlock.Details.FontIndex,
                TextAlignment = textBlock.Details.Align == DisplayBlock.Align.Left ? TextAlignment.Left :
                                                textBlock.Details.Align == DisplayBlock.Align.Center ? TextAlignment.Center :
                                                textBlock.Details.Align == DisplayBlock.Align.Right ? TextAlignment.Right : TextAlignment.Left
            };
            if (TryToParseRGB(textBlock.Details.BackColor, out colorHex))
            {
                label.Background = (Media.Brush)bc.ConvertFrom(colorHex);
            }
            if (TryToParseRGB(textBlock.Details.TextColor, out colorHex))
            {
                label.Foreground = (Media.Brush)bc.ConvertFrom(colorHex);
            }
            Canvas.SetTop(label, textBlock.Top);
            Canvas.SetLeft(label, textBlock.Left);
            Panel.SetZIndex(label, textBlock.ZIndex);
            return label;
        }

        private bool TryToParseRGB(string colorString, out string colorHexString)
        {
            if (Regex.IsMatch(colorString, @"\#\w{6}"))
            {
                colorHexString = colorString;
                return true;
            }
            var regular = @"^rgb\(\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(\d{1,3})\)$";
            colorHexString = string.Empty;
            if (Regex.IsMatch(colorString, regular))
            {
                var t = Regex.Matches(colorString, regular);
                var group = t[0].Groups;
                var red = Convert.ToByte(group[1].Value);
                var green = Convert.ToByte(group[2].Value);
                var blue = Convert.ToByte(group[3].Value);
                colorHexString = $"#{red:x2}{green:x2}{blue:x2}";
                return true;
            }
            else
            {
                regular = @"^rgba\(\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(\d{1,3})\,\s?(0\.\d{1,2})\)$";
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
}

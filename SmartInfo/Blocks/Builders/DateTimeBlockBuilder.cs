using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DisplayBlock = DomainObjects.Blocks;
using Media = System.Windows.Media;

namespace SmartInfo.Blocks.Builders
{
    public class DateTimeBlockBuilder : AbstractBuilder
    {
        private Timer _timer;
        private DisplayBlock.DateTimeBlock datetimeBlock;
        private TextBlock label;

        public override UIElement BuildElement(DisplayBlock.DisplayBlock displayBlock)
        {
            datetimeBlock = displayBlock as DisplayBlock.DateTimeBlock;
            var bc = new Media.BrushConverter();
            label = new TextBlock
            {
                Height = datetimeBlock.Height,
                Width = datetimeBlock.Width,
                TextWrapping = TextWrapping.Wrap,
                FontSize = datetimeBlock.Details.FontSize,
                FontFamily = new Media.FontFamily(datetimeBlock.Details.FontName),
                FontWeight = datetimeBlock.Details.Bold ? FontWeights.Bold : FontWeights.Normal,
                FontStyle = datetimeBlock.Details.Italic ? FontStyles.Italic : FontStyles.Normal,
                LineHeight = datetimeBlock.Details.FontSize * datetimeBlock.Details.FontIndex,
                TextAlignment = datetimeBlock.Details.Align == DisplayBlock.Align.Left ? TextAlignment.Left :
                                                datetimeBlock.Details.Align == DisplayBlock.Align.Center ? TextAlignment.Center :
                                                datetimeBlock.Details.Align == DisplayBlock.Align.Right ? TextAlignment.Right : TextAlignment.Left
            };
            if (datetimeBlock.Details.Format != null)
            {
                if (_timer == null)
                {
                    _timer = new Timer
                    {
                        AutoReset = true,
                        Interval = 500,
                        Enabled = true
                    };
                }

                _timer.Elapsed += timer_Elapsed;
            }
            if (ColorConverter.TryToParseRGB(datetimeBlock.Details.BackColor, out string colorHex))
            {
                label.Background = (Media.Brush)bc.ConvertFrom(colorHex);
            }
            if (ColorConverter.TryToParseRGB(datetimeBlock.Details.TextColor, out colorHex))
            {
                label.Foreground = (Media.Brush)bc.ConvertFrom(colorHex);
            }
            Canvas.SetTop(label, datetimeBlock.Top);
            Canvas.SetLeft(label, datetimeBlock.Left);
            Panel.SetZIndex(label, datetimeBlock.ZIndex);
            return label;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (datetimeBlock.Details == null)
            {
                _timer.Stop();
                _timer.Elapsed -= timer_Elapsed;
                _timer.Dispose();
                _timer = null;
                return;
            }

            _dispatcher.Invoke(() =>
            label.Text = DateTime.Now.ToString(datetimeBlock.Details.Format.ShowtimeFormat));
        }
    }
}

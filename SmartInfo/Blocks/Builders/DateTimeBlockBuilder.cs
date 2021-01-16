using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using DisplayBlock = DomainObjects.Blocks;
using Media = System.Windows.Media;

namespace SmartInfo.Blocks.Builders
{
    public class DateTimeBlockBuilder : AbstractBuilder
    {
        public override UIElement BuildElement(DisplayBlock.DisplayBlock displayBlock)
        {
            DatetimeBlockTimer timer;
            var datetimeBlock = displayBlock as DisplayBlock.DateTimeBlock;
            var bc = new Media.BrushConverter();
            var label = new TextBlock
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
                timer = new DatetimeBlockTimer(datetimeBlock, label)
                {
                    AutoReset = true,
                    Interval = 500,
                    Enabled = true,
                };

                timer.Elapsed += Timer_Elapsed;
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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var timer = sender as DatetimeBlockTimer;
            if (timer.Datetimeblock.Details == null)
            {
                timer.Stop();
                timer.Elapsed -= Timer_Elapsed;
                timer.Dispose();
                timer = null;
                return;
            }

            _dispatcher.Invoke(() =>
            timer.Label.Text = DateTime.Now.ToString(timer.Datetimeblock.Details.Format.ShowtimeFormat));
        }

        private class DatetimeBlockTimer : Timer
        {
            public DatetimeBlockTimer(DisplayBlock.DateTimeBlock datetimeblock, TextBlock label)
                : base()
            {
                Datetimeblock = datetimeblock;
                Label = label;
            }

            public DisplayBlock.DateTimeBlock Datetimeblock { get; }
            public TextBlock Label { get; }
        }

    }
}

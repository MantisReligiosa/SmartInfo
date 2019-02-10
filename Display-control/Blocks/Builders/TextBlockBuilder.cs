using System.Windows;
using System.Windows.Controls;
using DisplayBlock = DomainObjects.Blocks;
using Media = System.Windows.Media;

namespace Display_control.Blocks.Builders
{
    public class TextBlockBuilder : AbstractBuilder
    {
        public override UIElement BuildElement(DisplayBlock.DisplayBlock displayBlock)
        {
            var textBlock = displayBlock as DisplayBlock.TextBlock;
            var bc = new Media.BrushConverter();
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
            if (ColorConverter.TryToParseRGB(textBlock.Details.BackColor, out string colorHex))
            {
                label.Background = (Media.Brush)bc.ConvertFrom(colorHex);
            }
            if (ColorConverter.TryToParseRGB(textBlock.Details.TextColor, out colorHex))
            {
                label.Foreground = (Media.Brush)bc.ConvertFrom(colorHex);
            }
            Canvas.SetTop(label, textBlock.Top);
            Canvas.SetLeft(label, textBlock.Left);
            Panel.SetZIndex(label, textBlock.ZIndex);
            return label;
        }
    }
}

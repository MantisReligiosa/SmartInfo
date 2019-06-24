using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DisplayBlock = DomainObjects.Blocks;
using Media = System.Windows.Media;


namespace Display_control.Blocks.Builders
{
    public class PictureBlockBuilder : AbstractBuilder
    {
        public override UIElement BuildElement(DisplayBlock.DisplayBlock displayBlock)
        {
            var pictureBlock = displayBlock as DisplayBlock.PictureBlock;
            var bitmap = new Media.Imaging.BitmapImage();
            if (pictureBlock.Details.Base64Image != null)
            {
                using (var ms = new MemoryStream(Convert.FromBase64String(pictureBlock.Details.Base64Image)))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = Media.Imaging.BitmapCacheOption.OnLoad; // here
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                }
            }
            var image = new Image
            {
                Height = pictureBlock.Height,
                Width = pictureBlock.Width,
                Source = bitmap,
                Stretch = Media.Stretch.None
            };

            Canvas.SetTop(image, pictureBlock.Top);
            Canvas.SetLeft(image, pictureBlock.Left);
            Panel.SetZIndex(image, pictureBlock.ZIndex);
            return image;
        }
    }
}

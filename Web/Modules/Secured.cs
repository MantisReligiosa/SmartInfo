using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using ServiceInterfaces;
using System.Linq;
using Web.Models;
using Web.Models.Blocks;

namespace Web.Modules
{
    public class Secured : NancyModule
    {
        public Secured(
            IScreenController screenController,
            IBlockController blockController
            )
        {

            this.RequiresAuthentication();
            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];
            Get["/"] = parameters => View["Views/Home/Master.cshtml"];
            Post["/api/fonts"] = parameters =>
            {
                return Response.AsJson(new FontInfo
                {
                    Fonts = new Font[] { new Font { Id = 1, Name = "Font1" }, new Font { Id = 2, Name = "Font2" } },
                    FonSizes = new[] { 8, 10 }
                });
            };
            Post["/api/screenResolution"] = parameters =>
            {
                var data = this.Bind<ScreenResolutionRequest>();
                if (!data.RefreshData)
                {
                    var screenInfo = screenController.GetDatabaseScreenInfo();
                    if (screenInfo == null)
                    {
                        screenInfo = screenController.GetSystemScreenInfo();
                        screenController.SetDatabaseScreenInfo(screenInfo);
                    }
                    return Response.AsJson(screenInfo);
                }
                else
                {
                    var screenInfo = screenController.GetSystemScreenInfo();
                    screenController.SetDatabaseScreenInfo(screenInfo);
                    return Response.AsJson(screenInfo);
                }
            };
            Post["/api/setBackground"] = parameters =>
            {
                var data = this.Bind<ScreenBackgroundRequest>();
                screenController.SetBackground(data.Color);
                return Response.AsJson(true);
            };
            Get["/api/background"] = parameters =>
            {
                return Response.AsJson(screenController.GetBackground());
            };
            Post["/api/addTextBlock"] = parameters =>
            {
                var textBlock = blockController.AddTextBlock();
                var block = new TextBlockDto
                {
                    Height = textBlock.Height,
                    Width = textBlock.Width,
                    Left = textBlock.Left,
                    Top = textBlock.Top,
                    Type = "text",
                    Id = textBlock.Id,
                    Text = textBlock.Details.Text
                };
                return Response.AsJson(block);
            };
            Get["/api/blocks"] = parameters =>
            {
            var blocks = blockController.GetBlocks().Select(b =>
            {
                if (b is TextBlock textBlock)
                {
                    var block = new TextBlockDto
                    {
                        Height = b.Height,
                        Id = b.Id,
                        Left = b.Left,
                        Top = b.Top,
                        Width = b.Width
                    };
                    block.Type = "text";
                    block.Text = textBlock.Details.Text;
                    return block;
                }
                return new BlockDto
                {
                    Height = b.Height,
                    Id = b.Id,
                    Left = b.Left,
                    Top = b.Top,
                    Width = b.Width
                };
            });
            return Response.AsJson(blocks);
        };
            Post["/api/saveBlock"] = parameters =>
            {
                //var data = this.Bind<SaveBlockRequest<BlockDto>>();
                var data = this.Bind<BlockDto>();
                if (data.Type.Equals("text", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var b = this.Bind<TextBlockDto>();
                    var block = new TextBlock
                    {
                        Height = b.Height,
                        Id = b.Id,
                        Left = b.Left,
                        Top = b.Top,
                        Width = b.Width,
                        Details = new TextBlockDetails
                        {
                            Text = b.Text
                        }
                    };
                    blockController.SaveTextBlock(block);
                }

                return Response.AsJson(true);
            };
        }
    }
}
using AutoMapper;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using ServiceInterfaces;
using System.Linq;
using Web.Models;
using Web.Models.Blocks;
using Web.Profiles;

namespace Web.Modules
{
    public class Secured : NancyModule
    {
        private IMapper _mapper = AutoMapperConfig.Mapper;

        public Secured(
            ISystemController systemController,
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
                    Fonts = systemController.GetFonts(),
                    FonSizes = systemController.GetFontSizes()
                });
            };
            Post["/api/screenResolution"] = parameters =>
            {
                var data = this.Bind<ScreenResolutionRequest>();
                if (!data.RefreshData)
                {
                    var screenInfo = systemController.GetDatabaseScreenInfo();
                    if (screenInfo == null)
                    {
                        screenInfo = systemController.GetSystemScreenInfo();
                        systemController.SetDatabaseScreenInfo(screenInfo);
                    }
                    return Response.AsJson(screenInfo);
                }
                else
                {
                    var screenInfo = systemController.GetSystemScreenInfo();
                    systemController.SetDatabaseScreenInfo(screenInfo);
                    return Response.AsJson(screenInfo);
                }
            };
            Post["/api/setBackground"] = parameters =>
            {
                var data = this.Bind<ScreenBackgroundRequest>();
                systemController.SetBackground(data.Color);
                return Response.AsJson(true);
            };
            Get["/api/background"] = parameters =>
            {
                return Response.AsJson(systemController.GetBackground());
            };
            Post["/api/addTextBlock"] = parameters =>
            {
                var textBlock = blockController.AddTextBlock();
                var block = _mapper.Map<TextBlockDto>(textBlock);
                return Response.AsJson(block);
            };
            Post["/api/addTableBlock"] = parameters =>
            {
                var tableBlock = blockController.AddTableBlock();
                var block = _mapper.Map<TableBlockDto>(tableBlock);
                return Response.AsJson(block);
            };
            Get["/api/blocks"] = parameters =>
            {
                var blocks = blockController.GetBlocks().Select(b =>
                {
                    if (b is TextBlock textBlock)
                    {
                        var block = _mapper.Map<TextBlockDto>(textBlock);
                        return block;
                    }
                    if (b is TableBlock tableBlock)
                    {
                        var block = _mapper.Map<TableBlockDto>(tableBlock);
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
                            Text = b.Text,
                            BackColor = b.BackColor,
                            TextColor = b.TextColor,
                            FontName = b.Font,
                            FontSize = b.FontSize,
                            Align = b.Align,
                            Italic = b.Italic,
                            Bold = b.Bold
                        }
                    };
                    blockController.SaveTextBlock(block);
                }

                return Response.AsJson(true);
            };
        }
    }
}
using AutoMapper;
using DomainObjects.Blocks;
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
            Post["/api/addPictureBlock"] = parameters =>
            {
                var pictureBlock = blockController.AddPictureBlock();
                var block = _mapper.Map<PictureBlockDto>(pictureBlock);
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
                    if (b is PictureBlock pictureBlock)
                    {
                        var block = _mapper.Map<PictureBlockDto>(pictureBlock);
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
                    var block = _mapper.Map<TextBlock>(b);
                    blockController.SaveTextBlock(block);
                }
                else if (data.Type.Equals("table", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var b = this.Bind<TableBlockDto>();
                    var block = _mapper.Map<TableBlock>(b);
                    blockController.SaveTableBlock(block);
                }
                else if (data.Type.Equals("picture", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var b = this.Bind<PictureBlockDto>();
                    var block = _mapper.Map<PictureBlock>(b);
                    blockController.SavePictureBlock(block);
                }

                return Response.AsJson(true);
            };
            Post["/api/deleteBlock"] = parameters =>
            {
                var data = this.Bind<BlockDto>();
                blockController.DeleteBlock(data.Id);
                return Response.AsJson(true);
            };
            Post["/api/copyBlock"] = parameters =>
            {
                var data = this.Bind<BlockDto>();
                if (data.Type.Equals("text", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var b = this.Bind<TextBlockDto>();
                    var block = _mapper.Map<TextBlock>(b);
                    return _mapper.Map<TextBlockDto>(blockController.CopyTextBlock(block));
                }
                else if (data.Type.Equals("table", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var b = this.Bind<TableBlockDto>();
                    var block = _mapper.Map<TableBlock>(b);
                    return _mapper.Map<TableBlockDto>(blockController.CopyTableBlock(block));
                }
                else if (data.Type.Equals("picture", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var b = this.Bind<PictureBlockDto>();
                    var block = _mapper.Map<PictureBlock>(b);
                    return _mapper.Map<PictureBlockDto>(blockController.CopyPictureBlock(block));
                }

                return Response.AsJson(true);
            };
        }
    }
}
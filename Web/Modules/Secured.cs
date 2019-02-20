using AutoMapper;
using DomainObjects.Blocks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using NLog;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
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
            IBlockController blockController,
            IOperationController operationController,
            ISerializationController serializationController
            )
        {
            this.RequiresAuthentication();
            var logger = LogManager.GetCurrentClassLogger();
            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];
            Get["/"] = parameters => View["Views/Home/Master.cshtml"];
            Post["/api/fonts"] = parameters =>
            {
                try
                {
                    return Response.AsJson(new FontInfo
                    {
                        Fonts = systemController.GetFonts(),
                        Sizes = systemController.GetFontSizes(),
                        Indexes = systemController.GetFontHeightIndex()
                    });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка загрузки шрифтов", ex);
                }
            };
            Post["/api/screenResolution"] = parameters =>
            {
                try
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
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка загрузки информации о экранах", ex);
                }
            };
            Post["/api/setBackground"] = parameters =>
            {
                try
                {
                    var data = this.Bind<ScreenBackgroundRequest>();
                    blockController.SetBackground(data.Color);
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка установки фона", ex);
                }
            };
            Get["/api/background"] = parameters =>
            {
                try
                {
                    return Response.AsJson(blockController.GetBackground());
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка загрузки фона", ex);
                }
            };
            Post["/api/addTextBlock"] = parameters =>
            {
                try
                {
                    var textBlock = blockController.AddTextBlock();
                    var block = _mapper.Map<TextBlockDto>(textBlock);
                    return Response.AsJson(block);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка добавления текстового блока", ex);
                }
            };
            Post["/api/addTableBlock"] = parameters =>
            {
                try
                {
                    var tableBlock = blockController.AddTableBlock();
                    var block = _mapper.Map<TableBlockDto>(tableBlock);
                    return Response.AsJson(block);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка добавления таблицы", ex);
                }
            };
            Post["/api/addPictureBlock"] = parameters =>
            {
                try
                {
                    var pictureBlock = blockController.AddPictureBlock();
                    var block = _mapper.Map<PictureBlockDto>(pictureBlock);
                    return Response.AsJson(block);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка добавления картинки", ex);
                }
            };
            Get["/api/blocks"] = parameters =>
            {
                try
                {
                    var blocks = GetBlocks(blockController);
                    return Response.AsJson(blocks);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка загрузки блоков", ex);
                }
            };
            Post["/api/moveAndResize"] = parameters =>
            {
                try
                {
                    var block = this.Bind<SizeAndPositionDto>();
                    blockController.MoveAndResizeBlock(block.Id, block.Height, block.Width, block.Left, block.Top);
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка изменения размеров и положения блока", ex);
                }
            };
            Post["/api/saveBlock"] = parameters =>
            {
                try
                {
                    var data = this.Bind<BlockDto>();
                    if (data.Type.Equals("text", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var b = this.Bind<TextBlockDto>();
                        var block = _mapper.Map<TextBlock>(b);
                        blockController.SaveTextBlock(block);
                    }
                    else if (data.Type.Equals("table", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var b = this.Bind<TableBlockDto>();
                        var block = _mapper.Map<TableBlock>(b);
                        blockController.SaveTableBlock(block);
                    }
                    else if (data.Type.Equals("picture", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var b = this.Bind<PictureBlockDto>();
                        var block = _mapper.Map<PictureBlock>(b);
                        blockController.SavePictureBlock(block);
                    }

                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка сохранения блоков", ex);
                }
            };
            Post["/api/deleteBlock"] = parameters =>
            {
                try
                {
                    var data = this.Bind<BlockDto>();
                    blockController.DeleteBlock(data.Id);
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {

                    logger.Error(ex);
                    throw new Exception("Ошибка удаления блоков", ex);
                }
            };
            Post["/api/copyBlock"] = parameters =>
            {
                try
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
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка копирования блоков", ex);
                }
            };
            Post["/api/startShow"] = parameters =>
            {
                try
                {
                    operationController.StartShow();
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка запуска презентации", ex);
                }
            };
            Post["/api/stopShow"] = parameters =>
            {
                try
                {
                    operationController.StopShow();
                    return Response.AsJson(true);

                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка остановки презентации", ex);
                }
            };
            Post["/api/parseCSV"] = parameters =>
            {
                try
                {
                    var linesSeparator = new char[] { '\r', '\n' };
                    var itemSeparator = ',';

                    var data = this.Bind<CsvDataDto>();
                    var lines = data.Text.Split(linesSeparator, StringSplitOptions.RemoveEmptyEntries);
                    var result = new CsvTableDto();
                    result.Header.AddRange(lines.First().Split(itemSeparator));
                    var rowIndex = 0;
                    foreach (var line in lines.Skip(1))
                    {
                        var cells = line.Split(itemSeparator);
                        var delta = cells.Length - result.Header.Count;
                        if (delta > 0)
                            for (int i = 0; i < delta; i++)
                            {
                                result.Header.Add(string.Empty);
                            }
                        result.Rows.Add(new RowDto
                        {
                            Index = rowIndex,
                            Cells = cells
                        });
                        rowIndex++;
                    }

                    return Response.AsJson(result);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка чтения csv", ex);
                }
            };
            Get["/api/downloadConfig"] = parameters =>
            {
                try
                {
                    var response = new Response
                    {
                        ContentType = "text/xml",
                        Contents = (stream) => serializationController.SerializeXML(new ConfigDto
                        {
                            Background = blockController.GetBackground(),
                            Blocks = GetBlocks(blockController).ToList()
                        }).CopyTo(stream)
                    };
                    response.Headers.Add("Content-Disposition", "attachment; filename=config.xml");
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка выгрузки конфигурации", ex);
                }
            };
            Post["/api/uploadConfig"] = parameters =>
            {
                try
                {
                    var data = this.Bind<ConfigDataDto>();
                    var configDto = serializationController.Deserialize<ConfigDto>(data.Text);
                    blockController.SetBackground(configDto.Background);
                    blockController.Cleanup();
                    foreach (var b in configDto.Blocks)
                    {
                        if (b is TextBlockDto textBlock)
                        {
                            var block = _mapper.Map<TextBlock>(textBlock);
                            blockController.SaveTextBlock(block);
                        }
                        if (b is TableBlockDto tableBlock)
                        {
                            var block = _mapper.Map<TableBlock>(tableBlock);
                            blockController.SaveTableBlock(block);
                        }
                        if (b is PictureBlockDto pictureBlock)
                        {
                            var block = _mapper.Map<PictureBlock>(pictureBlock);
                            blockController.SavePictureBlock(block);
                        }
                    }
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception("Ошибка загрузки конфигурации", ex);
                }
            };
        }

        private IEnumerable<BlockDto> GetBlocks(IBlockController blockController)
        {
            return blockController.GetBlocks().Select(b =>
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
        }
    }
}
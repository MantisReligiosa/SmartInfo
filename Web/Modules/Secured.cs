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
            Get["/api/fonts"] = GetFonts(systemController, logger);
            Get["/api/datetimeformats"] = GetDatetimeFormats(systemController, logger);
            Get["/api/screenResolution"] = GetScreenResolution(systemController, logger);
            Post["/api/setBackground"] = SetBackground(blockController, logger);
            Get["/api/background"] = GetBackground(blockController, logger);
            Post["/api/addTextBlock"] = AddTextBlock(blockController, logger);
            Post["/api/addTableBlock"] = AddTableBlock(blockController, logger);
            Post["/api/addPictureBlock"] = AddPictureBlock(blockController, logger);
            Post["/api/addDateTimeBlock"] = AddDatetimeBlock(blockController, logger);
            Get["/api/blocks"] = GetBlocks(blockController, logger);
            Post["/api/moveAndResize"] = MoveAndResize(blockController, logger);
            Post["/api/saveBlock"] = SaveBlock(blockController, logger);
            Post["/api/deleteBlock"] = DeleteBlock(blockController, logger);
            Post["/api/copyBlock"] = CopyBlock(blockController, logger);
            Post["/api/startShow"] = StartShow(operationController, logger);
            Post["/api/stopShow"] = StopShow(operationController, logger);
            Post["/api/parseCSV"] = ParseCSV(logger);
            Get["/api/downloadConfig"] = DownloadConfig(blockController, serializationController, logger);
            Post["/api/uploadConfig"] = UploadConfig(blockController, serializationController, logger);
        }

        private Func<dynamic, dynamic> UploadConfig(IBlockController blockController, ISerializationController serializationController, Logger logger)
        {
            return parameters =>
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
                    var exception = new Exception("Ошибка загрузки конфигурации", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> DownloadConfig(IBlockController blockController, ISerializationController serializationController, Logger logger)
        {
            return parameters =>
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
                    var exception = new Exception("Ошибка выгрузки конфигурации", ex);
                    logger.Error(exception);
                    throw exception;

                }
            };
        }

        private Func<dynamic, dynamic> ParseCSV(Logger logger)
        {
            return parameters =>
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
                    var exception = new Exception("Ошибка чтения csv", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> StopShow(IOperationController operationController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    operationController.StopShow();
                    return Response.AsJson(true);

                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка остановки полноэкранного режима", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> StartShow(IOperationController operationController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    operationController.StartShow();
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка запуска полноэкранного режима", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> CopyBlock(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                var copiers = new Dictionary<string, Func<object>>
                {
                    { "text" , () => CopyBlock<TextBlock, TextBlockDto>(b => blockController.CopyTextBlock(b)) },
                    { "table" , () => CopyBlock<TableBlock, TableBlockDto>(b => blockController.CopyTableBlock(b)) },
                    { "picture" , () => CopyBlock<PictureBlock, PictureBlockDto>(b => blockController.CopyPictureBlock(b)) },
                    { "datetime" , () => CopyBlock<DateTimeBlock, DateTimeBlockDto>(b => blockController.CopyDateTimeBlock(b)) }
                };

                try
                {
                    var data = this.Bind<BlockDto>();
                    return copiers.First(kvp => kvp.Key.Equals(data.Type, StringComparison.InvariantCultureIgnoreCase)).Value.Invoke();
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка копирования блоков", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> DeleteBlock(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var data = this.Bind<BlockDto>();
                    blockController.DeleteBlock(data.Id);
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка удаления блоков", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> SaveBlock(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                var savers = new Dictionary<string, Action>()
                {
                    { "text", () => SaveBlock<TextBlock, TextBlockDto>(b => blockController.SaveTextBlock(b)) },
                    { "table", () => SaveBlock<TableBlock, TableBlockDto>(b => blockController.SaveTableBlock(b)) },
                    { "picture", () => SaveBlock<PictureBlock, PictureBlockDto>(b => blockController.SavePictureBlock(b)) },
                    { "datetime", () => SaveBlock<DateTimeBlock, DateTimeBlockDto>(b => blockController.SaveDateTimeBlock(b)) }
                };

                try
                {
                    var data = this.Bind<BlockDto>();
                    savers.First(kvp => kvp.Key.Equals(data.Type, StringComparison.InvariantCultureIgnoreCase)).Value.Invoke();
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка сохранения блоков", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> MoveAndResize(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var block = this.Bind<SizeAndPositionDto>();
                    blockController.MoveAndResizeBlock(block.Id, block.Height, block.Width, block.Left, block.Top);
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка изменения размеров и положения блока", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> GetBlocks(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var blocks = GetBlocks(blockController);
                    return Response.AsJson(blocks);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка загрузки блоков", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> AddDatetimeBlock(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var dateTimeBlock = blockController.AddDateTimeBlock();
                    var block = _mapper.Map<DateTimeBlockDto>(dateTimeBlock);
                    return Response.AsJson(block);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка добавления блока даты/времени", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> AddPictureBlock(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var pictureBlock = blockController.AddPictureBlock();
                    var block = _mapper.Map<PictureBlockDto>(pictureBlock);
                    return Response.AsJson(block);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка добавления картинки", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> AddTableBlock(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var tableBlock = blockController.AddTableBlock();
                    var block = _mapper.Map<TableBlockDto>(tableBlock);
                    return Response.AsJson(block);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка добавления таблицы", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> AddTextBlock(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var textBlock = blockController.AddTextBlock();
                    var block = _mapper.Map<TextBlockDto>(textBlock);
                    return Response.AsJson(block);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка добавления текстового блока", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> GetBackground(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    return Response.AsJson(blockController.GetBackground());
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка загрузки фона", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> SetBackground(IBlockController blockController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    var data = this.Bind<ScreenBackgroundRequest>();
                    blockController.SetBackground(data.Color);
                    return Response.AsJson(true);
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка установки фона", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> GetScreenResolution(ISystemController systemController, Logger logger)
        {
            return parameters =>
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
                    var exception = new Exception("Ошибка загрузки информации о экранах", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> GetDatetimeFormats(ISystemController systemController, Logger logger)
        {
            return parameters =>
            {
                try
                {
                    return Response.AsJson(systemController.GetDatetimeFormats());
                }
                catch (Exception ex)
                {
                    var exception = new Exception("Ошибка загрузки форматов даты/времени", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private Func<dynamic, dynamic> GetFonts(ISystemController systemController, Logger logger)
        {
            return parameters =>
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
                    var exception = new Exception("Ошибка загрузки шрифтов", ex);
                    logger.Error(exception);
                    throw exception;
                }
            };
        }

        private void SaveBlock<TBlock, TBlockDto>(Action<TBlock> saveAction)
            where TBlock : DisplayBlock
            where TBlockDto : BlockDto
        {
            var b = this.Bind<TBlockDto>();
            var block = _mapper.Map<TBlock>(b);
            saveAction.Invoke(block);
        }

        private TBlockDto CopyBlock<TBlock, TBlockDto>(Func<TBlock, TBlock> copyFunction)
            where TBlock : DisplayBlock
            where TBlockDto : BlockDto
        {
            var b = this.Bind<TBlockDto>();
            var block = _mapper.Map<TBlock>(b);
            return _mapper.Map<TBlockDto>(copyFunction(block));
        }

        private IEnumerable<BlockDto> GetBlocks(IBlockController blockController)
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
                if (b is DateTimeBlock dateTimeBlock)
                {
                    var block = _mapper.Map<DateTimeBlockDto>(dateTimeBlock);
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
            return blocks;
        }
    }
}
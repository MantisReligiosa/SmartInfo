using AutoMapper;
using DomainObjects;
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
        private readonly ISystemController _systemController;
        private readonly IBlockController _blockController;
        private readonly IOperationController _operationController;
        private readonly ISerializationController _serializationController;
        private readonly ILogger _logger;

        private readonly Dictionary<string, Action> _savers;
        private readonly Dictionary<string, Func<object>> _copiers;

        public Secured(
            ISystemController systemController,
            IBlockController blockController,
            IOperationController operationController,
            ISerializationController serializationController
            )
        {
            this.RequiresAuthentication();
            _logger = LogManager.GetCurrentClassLogger();
            _systemController = systemController;
            _blockController = blockController;
            _operationController = operationController;
            _serializationController = serializationController;

            Get["/master"] = parameters => View["Views/Home/Master.cshtml"];
            Get["/"] = parameters => View["Views/Home/Master.cshtml"];
            Get["/api/fonts"] = Wrap(GetFonts, "Ошибка загрузки шрифтов");
            Get["/api/datetimeformats"] = Wrap(GetDatetimeFormats, "Ошибка загрузки форматов даты/времени");
            Get["/api/screenResolution"] = Wrap(GetScreenInfo, "Ошибка загрузки информации о экранах");
            Post["/api/setBackground"] = Wrap(SetBackground, "Ошибка установки фона");
            Get["/api/background"] = Wrap(GetBackground, "Ошибка загрузки фона");
            Post["/api/addTextBlock"] = Wrap(AddTextBlock, "Ошибка добавления текстового блока");
            Post["/api/addTableBlock"] = Wrap(AddTableBlock, "Ошибка добавления таблицы");
            Post["/api/addPictureBlock"] = Wrap(AddPictureBlock, "Ошибка добавления картинки");
            Post["/api/addDateTimeBlock"] = Wrap(AddDatetimeBlock, "Ошибка добавления блока даты/времени");
            Post["api/addMetaBlock"] = Wrap(AddMetaBlock, "Ошибка добавления метаблока");
            Get["/api/blocks"] = Wrap(GetBlocks, "Ошибка загрузки блоков");
            Post["/api/moveAndResize"] = Wrap(MoveAndResize, "Ошибка изменения размеров и положения блока");
            Post["/api/saveBlock"] = Wrap(SaveBlock, "Ошибка сохранения блоков");
            Post["/api/deleteBlock"] = Wrap(DeleteBlock, "Ошибка удаления блоков");
            Post["/api/copyBlock"] = Wrap(CopyBlock, "Ошибка копирования блоков");
            Post["/api/startShow"] = Wrap(StartShow, "Ошибка запуска полноэкранного режима");
            Post["/api/stopShow"] = Wrap(StopShow, "Ошибка остановки полноэкранного режима");
            Post["/api/parseCSV"] = Wrap(ParseCSV, "Ошибка чтения csv");
            Get["/api/downloadConfig"] = DownloadConfig(blockController, serializationController, _logger);
            Post["/api/uploadConfig"] = Wrap(UploadConfig, "Ошибка загрузки конфигурации");

            _savers = new Dictionary<string, Action>()
            {
                { "text", () => SaveBlock<TextBlock, TextBlockDto>(b => blockController.SaveTextBlock(b)) },
                { "table", () => SaveBlock<TableBlock, TableBlockDto>(b => blockController.SaveTableBlock(b)) },
                { "picture", () => SaveBlock<PictureBlock, PictureBlockDto>(b => blockController.SavePictureBlock(b)) },
                { "datetime", () => SaveBlock<DateTimeBlock, DateTimeBlockDto>(b => blockController.SaveDateTimeBlock(b)) }
            };
            _copiers = new Dictionary<string, Func<object>>
            {
                { "text" , () => CopyBlock<TextBlock, TextBlockDto>(b => blockController.CopyTextBlock(b)) },
                { "table" , () => CopyBlock<TableBlock, TableBlockDto>(b => blockController.CopyTableBlock(b)) },
                { "picture" , () => CopyBlock<PictureBlock, PictureBlockDto>(b => blockController.CopyPictureBlock(b)) },
                { "datetime" , () => CopyBlock<DateTimeBlock, DateTimeBlockDto>(b => blockController.CopyDateTimeBlock(b)) }
            };
        }

        private void UploadConfig()
        {
            var data = this.Bind<ConfigDataDto>();
            var configDto = _serializationController.Deserialize<ConfigDto>(data.Text);
            _blockController.SetBackground(configDto.Background);
            _blockController.Cleanup();
            foreach (var b in configDto.Blocks)
            {
                if (b is TextBlockDto textBlock)
                {
                    var block = _mapper.Map<TextBlock>(textBlock);
                    _blockController.SaveTextBlock(block);
                }
                if (b is TableBlockDto tableBlock)
                {
                    var block = _mapper.Map<TableBlock>(tableBlock);
                    _blockController.SaveTableBlock(block);
                }
                if (b is PictureBlockDto pictureBlock)
                {
                    var block = _mapper.Map<PictureBlock>(pictureBlock);
                    _blockController.SavePictureBlock(block);
                }
            }
        }

        private Func<dynamic, dynamic> DownloadConfig(IBlockController blockController, ISerializationController serializationController, ILogger logger)
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
                            Blocks = GetBlocks().ToList()
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

        private CsvTableDto ParseCSV()
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

            return result;
        }

        private void StopShow()
        {
            _operationController.StopShow();
        }

        private void StartShow()
        {
            _operationController.StartShow();
        }

        private object CopyBlock()
        {
            var data = this.Bind<BlockDto>();
            return _copiers.First(kvp => kvp.Key.Equals(data.Type, StringComparison.InvariantCultureIgnoreCase)).Value.Invoke();
        }

        private void DeleteBlock()
        {
            var data = this.Bind<BlockDto>();
            _blockController.DeleteBlock(data.Id);
        }

        private void SaveBlock()
        {
            var data = this.Bind<BlockDto>();
            _savers.First(kvp => kvp.Key.Equals(data.Type, StringComparison.InvariantCultureIgnoreCase)).Value.Invoke();
        }

        private void MoveAndResize()
        {
            var block = this.Bind<SizeAndPositionDto>();
            _blockController.MoveAndResizeBlock(block.Id, block.Height, block.Width, block.Left, block.Top);
        }

        private MetaBlockDto AddMetaBlock()
        {
            var metablock = _blockController.AddMetaBlock();
            var block = _mapper.Map<MetaBlockDto>(metablock);
            return block;
        }

        private DateTimeBlockDto AddDatetimeBlock()
        {
            var dateTimeBlock = _blockController.AddDateTimeBlock();
            var block = _mapper.Map<DateTimeBlockDto>(dateTimeBlock);
            return block;
        }

        private PictureBlockDto AddPictureBlock()
        {
            var pictureBlock = _blockController.AddPictureBlock();
            var block = _mapper.Map<PictureBlockDto>(pictureBlock);
            return block;
        }

        private TableBlockDto AddTableBlock()
        {
            var tableBlock = _blockController.AddTableBlock();
            var block = _mapper.Map<TableBlockDto>(tableBlock);
            return block;
        }

        private TextBlockDto AddTextBlock()
        {
            var textBlock = _blockController.AddTextBlock();
            var block = _mapper.Map<TextBlockDto>(textBlock);
            return block;
        }

        private string GetBackground()
        {
            return _blockController.GetBackground();
        }

        private void SetBackground()
        {
            var data = this.Bind<ScreenBackgroundRequest>();
            _blockController.SetBackground(data.Color);
        }

        private ScreenInfo GetScreenInfo()
        {
            var data = this.Bind<ScreenResolutionRequest>();
            ScreenInfo screenInfo;
            if (!data.RefreshData)
            {
                screenInfo = _systemController.GetDatabaseScreenInfo();
                if (screenInfo == null)
                {
                    screenInfo = _systemController.GetSystemScreenInfo();
                    _systemController.SetDatabaseScreenInfo(screenInfo);
                }
            }
            else
            {
                screenInfo = _systemController.GetSystemScreenInfo();
                _systemController.SetDatabaseScreenInfo(screenInfo);
            }
            return screenInfo;
        }

        private IEnumerable<DateTimeFormat> GetDatetimeFormats()
        {
            return _systemController.GetDatetimeFormats();
        }

        private FontInfo GetFonts()
        {
            return new FontInfo
            {
                Fonts = _systemController.GetFonts(),
                Sizes = _systemController.GetFontSizes(),
                Indexes = _systemController.GetFontHeightIndex()
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

        private IEnumerable<BlockDto> GetBlocks()
        {
            var blocks = _blockController.GetBlocks().Select(b =>
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

        private Func<dynamic, dynamic> Wrap<TResponceModel>(Func<TResponceModel> func, string errorMsg)
        {
            try
            {
                return parameters =>
                {
                    return Response.AsJson(func.Invoke());
                };
            }
            catch (Exception ex)
            {
                var exception = new Exception(errorMsg, ex);
                _logger.Error(exception);
                throw exception;
            }
        }

        private Func<dynamic, dynamic> Wrap(Action action, string errorMsg)
        {
            return Wrap(() =>
            {
                action.Invoke();
                return true;
            },
            errorMsg);
        }
    }
}
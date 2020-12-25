using DomainObjects.Blocks;
using Nancy;
using Nancy.ModelBinding;
using NLog;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Web.Models.Blocks;
using Web.Models.Blocks.Converter;
using Web.Profiles;

namespace Web.Modules
{
    public class BlockModule : WrappedNancyModule
    {
        private readonly IBlockController _blockController;
        private readonly ISerializationController _serializationController;
        private readonly IExcelTableProvider _tableProvider;

        private readonly Dictionary<string, Func<BlockDto, dynamic>> _savers;
        private readonly Dictionary<string, Func<object>> _copiers;
        private readonly Dictionary<string, Func<BlockDto, DisplayBlock>> _blockDtoConververs;

        public BlockModule(
            IBlockController blockController,
            ISerializationController serializationController,
            IExcelTableProvider tableProvider
            )
            : base()
        {
            _blockController = blockController;
            _serializationController = serializationController;
            _tableProvider = tableProvider;

            Post["/api/setBackground"] = Wrap(SetBackground, "Ошибка установки фона");
            Get["/api/background"] = Wrap(GetBackground, "Ошибка загрузки фона");
            Post["/api/addTextBlock"] = Wrap(AddTextBlock, "Ошибка добавления текстового блока");
            Post["/api/addTableBlock"] = Wrap(AddTableBlock, "Ошибка добавления таблицы");
            Post["/api/addPictureBlock"] = Wrap(AddPictureBlock, "Ошибка добавления картинки");
            Post["/api/addDateTimeBlock"] = Wrap(AddDatetimeBlock, "Ошибка добавления блока даты/времени");
            Post["api/addMetaBlock"] = Wrap(AddScenario, "Ошибка добавления метаблока");
            Get["/api/blocks"] = Wrap(GetBlocks, "Ошибка загрузки блоков");
            Post["/api/moveAndResize"] = Wrap(MoveAndResize, "Ошибка изменения размеров и положения блока");
            Post["/api/saveBlock"] = Wrap(SaveBlock, "Ошибка сохранения блоков");
            Post["/api/deleteBlock"] = Wrap(DeleteBlock, "Ошибка удаления блоков");
            Post["/api/copyBlock"] = Wrap(CopyBlock, "Ошибка копирования блоков");
            Post["/api/parseTable"] = Wrap(ParseTable, "Ошибка чтения таблицы");
            Get["/api/downloadConfig"] = DownloadConfig(blockController, serializationController, _logger);
            Post["/api/uploadConfig"] = Wrap(UploadConfig, "Ошибка загрузки конфигурации");
            Post["/api/cleanup"] = Wrap(Cleanup, "Ошибка удаления блоков");

            _savers = new Dictionary<string, Func<BlockDto, dynamic>>()
            {
                { BlockType.Text, (dto) => SaveBlock<TextBlock, TextBlockDto>(dto, b => _mapper.Map<TextBlockDto>(blockController.SaveTextBlock(b))) },
                { BlockType.Table, (dto) => SaveBlock<TableBlock, TableBlockDto>(dto, b => _mapper.Map<TableBlockDto>(blockController.SaveTableBlock(b))) },
                { BlockType.Picture, (dto) => SaveBlock<PictureBlock, PictureBlockDto>(dto, b => _mapper.Map<PictureBlockDto>(blockController.SavePictureBlock(b))) },
                { BlockType.Datetime, (dto) => SaveBlock<DateTimeBlock, DateTimeBlockDto>(dto, b => _mapper.Map<DateTimeBlockDto>(blockController.SaveDateTimeBlock(b))) },
                { BlockType.Meta, (dto) => SaveBlock<Scenario, ScenarioDto>(dto, b => _mapper.Map<ScenarioDto>(blockController.SaveScenario(b)), CastBlocksInFrames) }
            };
            _copiers = new Dictionary<string, Func<object>>
            {
                { BlockType.Text , () => CopyBlock<TextBlock, TextBlockDto>(b => blockController.CopyTextBlock(b)) },
                { BlockType.Table , () => CopyBlock<TableBlock, TableBlockDto>(b => blockController.CopyTableBlock(b)) },
                { BlockType.Picture , () => CopyBlock<PictureBlock, PictureBlockDto>(b => blockController.CopyPictureBlock(b)) },
                { BlockType.Datetime , () => CopyBlock<DateTimeBlock, DateTimeBlockDto>(b => blockController.CopyDateTimeBlock(b)) },
                { BlockType.Meta, () => CopyBlock<Scenario, ScenarioDto>(b => blockController.CopyScenario(b)) }
            };
            _blockDtoConververs = new Dictionary<string, Func<BlockDto, DisplayBlock>>
            {
                { BlockType.Text, dto => _mapper.Map<TextBlock>(dto as TextBlockDto) },
                { BlockType.Table, dto => _mapper.Map<TableBlock>(dto as TableBlockDto) },
                { BlockType.Picture, dto => _mapper.Map<PictureBlock>(dto as PictureBlockDto) },
                { BlockType.Datetime, dto => _mapper.Map<DateTimeBlock>(dto as DateTimeBlockDto) }
            };
        }

        private void CastBlocksInFrames(BlockDto blockDto, Scenario scenario)
        {
            var scenarioDto = blockDto as ScenarioDto;
            foreach (var sceneDto in scenarioDto.Scenes)
            {
                var scene = scenario.Details.Scenes.FirstOrDefault(f => f.Id.Equals(sceneDto.Id));
                scene.Blocks = sceneDto.Blocks?.Select(frameBlockDto =>
                     _blockDtoConververs
                        .First(kvp => kvp.Key.Equals(frameBlockDto.Type, StringComparison.InvariantCultureIgnoreCase))
                            .Value.Invoke(frameBlockDto)
                ).ToList() ?? new List<DisplayBlock>();
            }
        }

        private void Cleanup()
        {
            _blockController.Cleanup();
        }

        private void UploadConfig()
        {
            var data = this.Bind<ConfigDataDto>();
            var configDto = _serializationController.DeserializeXML<ConfigDto>(data.Text);
            _blockController.SetBackground(configDto.Background);
            _blockController.Cleanup();
            var blocks = configDto.Blocks;
            SaveBlocks(blocks);
        }

        private void SaveBlocks(List<BlockDto> blocks)
        {
            foreach (var b in blocks)
            {
                switch (b)
                {
                    case TextBlockDto textBlockDto:
                        var textBlock = _mapper.Map<TextBlock>(textBlockDto);
                        _blockController.SaveTextBlock(textBlock);
                        break;
                    case TableBlockDto tableBlockDto:
                        var tableBlock = _mapper.Map<TableBlock>(tableBlockDto);
#warning Не сохраняются RowHeights
                        _blockController.SaveTableBlock(tableBlock);
                        break;
                    case PictureBlockDto pictureBlockDto:
                        var pictureBlock = _mapper.Map<PictureBlock>(pictureBlockDto);
                        _blockController.SavePictureBlock(pictureBlock);
                        break;
                    case DateTimeBlockDto dateTimeBlockDto:
                        var datetimeBlock = _mapper.Map<DateTimeBlock>(dateTimeBlockDto);
                        _blockController.SaveDateTimeBlock(datetimeBlock);
                        break;
                    case ScenarioDto scenarioDto:
                        var scenario = _mapper.Map<Scenario>(scenarioDto);
                        var savedScenario = _blockController.SaveScenario(scenario);
                        foreach (var scene in scenarioDto.Scenes.Where(f => f.Blocks?.Any() ?? false))
                        {
                            var savedScene = savedScenario.Details.Scenes.Single(s => s.Index == scene.Index);
                            SaveBlocks(scene.Blocks.Select(blockInScene=>
                            {
                                blockInScene.MetablockFrameId = savedScene.Id;
                                return blockInScene;
                            }).ToList());
                        }
                        break;
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

        private ParsedTableDto ParseTable()
        {
            var bindedData = this.Bind<TableDataDto>();
            TableType tableType = TableType.Unknown;
            if (bindedData.Extension.Equals("csv", StringComparison.InvariantCultureIgnoreCase))
                tableType = TableType.CSV;
            else if (bindedData.Extension.Equals("xls", StringComparison.InvariantCultureIgnoreCase))
                tableType = TableType.Excel;
            _tableProvider.LoadData(bindedData.Context, tableType);
            var result = new ParsedTableDto();
            result.Header.AddRange(_tableProvider.Header);
            var rowIndex = 0;
            foreach (var cells in _tableProvider.Rows)
            {
                var delta = cells.Count() - result.Header.Count;
                if (delta > 0)
                    for (int i = 0; i < delta; i++)
                    {
                        result.Header.Add(string.Empty);
                    }
                result.Rows.Add(new RowDto
                {
                    Index = rowIndex,
                    Cells = cells.ToArray()
                });
                rowIndex++;
            }
            return result;
        }

        private object CopyBlock()
        {
            var data = this.Bind<BlockDto>();
            return _copiers.First(kvp => kvp.Key.Equals(data.Type, StringComparison.InvariantCultureIgnoreCase)).Value.Invoke();
        }

        private void DeleteBlock()
        {
            var data = this.Bind<BlockDto>();
            _blockController.DeleteBlock(BlockIdProcessor.FromDTOId(data.Id));
        }

        private dynamic SaveBlock()
        {
            var data = this.Bind<BlockDto>(new BindingConfig
            {
                IgnoreErrors = true
            });
            return _savers.First(kvp => kvp.Key.Equals(data.Type, StringComparison.InvariantCultureIgnoreCase)).Value.Invoke(data);
        }

        private void MoveAndResize()
        {
            var block = this.Bind<SizeAndPositionDto>();
            _blockController.MoveAndResizeBlock(BlockIdProcessor.FromDTOId(block.Id), block.Height, block.Width, block.Left, block.Top);
        }

        private ScenarioDto AddScenario()
        {
            var scenario = _blockController.AddScenario();
            var block = _mapper.Map<ScenarioDto>(scenario);
            return block;
        }

        private DateTimeBlockDto AddDatetimeBlock()
        {
            var addBlockParams = this.Bind<AddBlockParamsDto>();
            var dateTimeBlock = _blockController.AddDateTimeBlock(addBlockParams.FrameId);
            var block = _mapper.Map<DateTimeBlockDto>(dateTimeBlock);
            return block;
        }

        private PictureBlockDto AddPictureBlock()
        {
            var addBlockParams = this.Bind<AddBlockParamsDto>();
            var pictureBlock = _blockController.AddPictureBlock(addBlockParams.FrameId);
            var block = _mapper.Map<PictureBlockDto>(pictureBlock);
            return block;
        }

        private TableBlockDto AddTableBlock()
        {
            var addBlockParams = this.Bind<AddBlockParamsDto>();
            var tableBlock = _blockController.AddTableBlock(addBlockParams.FrameId);
            var block = _mapper.Map<TableBlockDto>(tableBlock);
            return block;
        }

        private TextBlockDto AddTextBlock()
        {
            var addBlockParams = this.Bind<AddBlockParamsDto>();
            var textBlock = _blockController.AddTextBlock(addBlockParams.FrameId);
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

        private dynamic SaveBlock<TBlock, TBlockDto>(BlockDto dto, Func<TBlock, dynamic> saveAction, Action<BlockDto, TBlock> afterMapping = null)
            where TBlock : DisplayBlock
            where TBlockDto : BlockDto
        {
            var b = dto as TBlockDto;
            var block = _mapper.Map<TBlock>(b);
            if (afterMapping != null)
            {
                afterMapping.Invoke(dto, block);
            }
            return saveAction.Invoke(block);
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
                return _mapper.Map(b, b.GetType(), typeof(BlockDto)) as BlockDto;
            });
            return blocks;
        }
    }
}
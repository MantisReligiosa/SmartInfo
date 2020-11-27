using NSubstitute;
using NUnit.Framework;
using Repository.Entities;
using Repository.Entities.DetailsEntities;
using Repository.Entities.DetailsEntities.TableBlockRowDetailsEntities;
using Repository.Entities.DisplayBlockEntities;
using Repository.Repositories;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryTests
{
    [TestFixture]
    public class DisplayBlockRepositoryTests
    {
        private IDatabaseContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IDatabaseContext>();
        }


        [Test]
        public void LoadBlocksTest()
        {
            #region Построение
            var textBlockEntity = CreateTextBlockEntity();
            var pictureBlockEntity = CreatePictureBlockEntity();
            var dateTimeBlockEntity = CreateDateTimeBlockEntity();
            var tableBlockEntity = CreateTableBlockEntity();

            InitGetter(new List<TextBlockEntity> { textBlockEntity });
            InitGetter(new List<PictureBlockEntity> { pictureBlockEntity });
            InitGetter(new List<DateTimeBlockEntity> { dateTimeBlockEntity });
            InitGetter(new List<TableBlockEntity> { tableBlockEntity });
            #endregion

            var repository = new DisplayBlockRepository(_context);
            var blocks = repository.GetAllNonScenaried();
        }

        [Test]
        public void LoadScenarioTest()
        {
            var textBlockEntity = CreateTextBlockEntity();
            var pictureBlockEntity = CreatePictureBlockEntity();
            var dateTimeBlockEntity = CreateDateTimeBlockEntity();
            var tableBlockEntity = CreateTableBlockEntity();

            var scenarioEntity = CreateScenarioEntity();
            scenarioEntity.ScenarioDetails.Scenes.First().DisplayBlocks.Add(textBlockEntity);
            scenarioEntity.ScenarioDetails.Scenes.First().DisplayBlocks.Add(pictureBlockEntity);
            scenarioEntity.ScenarioDetails.Scenes.Last().DisplayBlocks.Add(dateTimeBlockEntity);
            scenarioEntity.ScenarioDetails.Scenes.Last().DisplayBlocks.Add(tableBlockEntity);

            InitGetter(new List<ScenarioEntity> { scenarioEntity });

            var repository = new DisplayBlockRepository(_context);
            var blocks = repository.GetAllNonScenaried();
        }

        private ScenarioEntity CreateScenarioEntity()
        {
            var scenarioEntity = new ScenarioEntity
            {
                Caption = "ScenarioEntity1",
                Height = 1,
                Id = 2,
                Left = 3,
                Scene = null,
                SceneId = null,
                Top = 4,
                Width = 5,
                ZIndex = 6
            };
            var scenarioDetailsEntity = new ScenarioDetailsEntity
            {
                Id = 10,
                ScenarioEntity = scenarioEntity
            };
            scenarioEntity.ScenarioDetails = scenarioDetailsEntity;

            var scene1 = new SceneEntity
            {
                DateToUse = 0,
                Id = 1,
                Duration = 10,
                Index = 1,
                Name = "scene1",
                ScenarioDetailsEntity = scenarioDetailsEntity,
                ScenarioDetailsEntityId = scenarioDetailsEntity.Id,
                UseFromTime = 0,
                UseInDayOfWeek = 0,
                UseInDayOfWeekFlags = 0,
                UseInTimeInterval = 0,
                UseToTime = 0,
                DisplayBlocks = new List<DisplayBlockEntity>()
            };

            var scene2 = new SceneEntity
            {
                DateToUse = 1606384800, //26.11.2020 10:00:00
                Id = 1,
                Duration = 10,
                Index = 1,
                Name = "scene2",
                ScenarioDetailsEntity = scenarioDetailsEntity,
                ScenarioDetailsEntityId = scenarioDetailsEntity.Id,
                UseFromTime = 36000, //10:00
                UseInDayOfWeek = 1,
                UseInDayOfWeekFlags = 0b01010101, //Пн, ср, пт, вс
                UseInTimeInterval = 1,
                UseToTime = 43200, //12:00
                DisplayBlocks = new List<DisplayBlockEntity>()
            };

            scenarioDetailsEntity.Scenes = new List<SceneEntity> { scene1, scene2 };

            return scenarioEntity;
        }

        private TableBlockEntity CreateTableBlockEntity()
        {
            var tableBlockEntity = new TableBlockEntity
            {
                Caption = "TableBlockEntity1",
                Height = 1,
                Id = 2,
                Left = 3,
                Scene = null,
                SceneId = null,
                Top = 4,
                Width = 5,
                ZIndex = 6
            };
            var tableBlockDetailsEntity = new TableBlockDetailsEntity
            {
                FontIndex = 1.1M,
                FontName = "Arial",
                FontSize = 14,
                Id = 121,
                TableBlockEntity = tableBlockEntity
            };
            tableBlockEntity.TableBlockDetails = tableBlockDetailsEntity;

            tableBlockDetailsEntity.CellDetailsEntities = new List<TableBlockCellDetailsEntity>
            {
                new TableBlockCellDetailsEntity
                {
                    Id = 1, Column = 1, Row = 1, Value="A",TableBlockDetailsEntity = tableBlockDetailsEntity, TableBlockDetailsEntityId = tableBlockDetailsEntity.Id
                },
                new TableBlockCellDetailsEntity
                {
                    Id = 2, Column = 2, Row = 1, Value="B",TableBlockDetailsEntity = tableBlockDetailsEntity, TableBlockDetailsEntityId = tableBlockDetailsEntity.Id
                },
                new TableBlockCellDetailsEntity
                {
                    Id = 3, Column = 1, Row = 2, Value="A1",TableBlockDetailsEntity = tableBlockDetailsEntity, TableBlockDetailsEntityId = tableBlockDetailsEntity.Id
                },
                new TableBlockCellDetailsEntity
                {
                    Id = 4, Column = 2, Row = 2, Value="B1",TableBlockDetailsEntity = tableBlockDetailsEntity, TableBlockDetailsEntityId = tableBlockDetailsEntity.Id
                }
            };

            tableBlockDetailsEntity.ColumnWidthEntities = new List<TableBlockColumnWidthEntity>
            {
                new TableBlockColumnWidthEntity{Id = 1, Index = 1, Units = 0, Value = null, TableBlockDetailsEntity = tableBlockDetailsEntity,
                    TableBlockDetailsEntityId = tableBlockDetailsEntity.Id},
                new TableBlockColumnWidthEntity{Id = 2, Index = 2, Units = 1, Value = 100, TableBlockDetailsEntity = tableBlockDetailsEntity,
                    TableBlockDetailsEntityId = tableBlockDetailsEntity.Id}
            };

            tableBlockDetailsEntity.RowHeightsEntities = new List<TableBlockRowHeightEntity>
            {
                new TableBlockRowHeightEntity{Id = 1, Index = 1, Units = 0, Value = null, TableBlockDetailsEntity = tableBlockDetailsEntity,
                    TableBlockDetailsEntityId = tableBlockDetailsEntity.Id},
                new TableBlockRowHeightEntity{Id = 2, Index = 2, Units = 1, Value = 100, TableBlockDetailsEntity = tableBlockDetailsEntity,
                    TableBlockDetailsEntityId = tableBlockDetailsEntity.Id}
            };

            tableBlockDetailsEntity.RowDetailsEntities = new List<TableBlockRowDetailsEntity>
            {
                new TableBlockHeaderDetailsEntity{Align = 1, BackColor="111",Bold = 0, Id = 1, Italic = 1, TextColor="222", 
                    TableBlockDetailsEntity = tableBlockDetailsEntity, TableBlockDetailsEntityId = tableBlockDetailsEntity.Id },
                new TableBlockEvenRowDetailsEntity{Align = 0, BackColor="000",Bold = 0, Id = 2, Italic = 1, TextColor="123",
                    TableBlockDetailsEntity = tableBlockDetailsEntity, TableBlockDetailsEntityId = tableBlockDetailsEntity.Id },
                new TableBlockOddRowDetailsEntity{Align = 0, BackColor="0a0",Bold = 0, Id = 3, Italic = 1, TextColor="bbb",
                    TableBlockDetailsEntity = tableBlockDetailsEntity, TableBlockDetailsEntityId = tableBlockDetailsEntity.Id }
            };

            return tableBlockEntity;
        }

        private static PictureBlockEntity CreatePictureBlockEntity()
        {
            var pictureBlockEntity = new PictureBlockEntity
            {
                Caption = "PictureBlockEntity1",
                Height = 1,
                Id = 2,
                Left = 3,
                Scene = null,
                SceneId = null,
                Top = 4,
                Width = 5,
                ZIndex = 6
            };
            var pictureBlockDetailsEntity = new PictureBlockDetailsEntity
            {
                Id = 1,
                Base64Image = "AAA",
                ImageMode = 1,
                SaveProportions = 1,
                PictureBlockEntity = pictureBlockEntity
            };
            pictureBlockEntity.PictureBlockDetails = pictureBlockDetailsEntity;
            return pictureBlockEntity;
        }

        private static TextBlockEntity CreateTextBlockEntity()
        {
            var textBlockEntity = new TextBlockEntity
            {
                Caption = "TextBlockEntity1",
                Height = 40,
                Id = 1,
                Left = 10,
                Scene = null,
                SceneId = null,
                Top = 15,
                Width = 20,
                ZIndex = 1,
            };
            var textBlockDetails = new TextBlockDetailsEntity
            {
                Align = 1,
                BackColor = "#aaa",
                Bold = 1,
                FontIndex = 1.2M,
                FontName = "Arial",
                FontSize = 10,
                Id = 1,
                Italic = 1,
                Text = "TEXT!!!!",
                TextColor = "#111",
                TextBlockEntity = textBlockEntity
            };
            textBlockEntity.TextBlockDetails = textBlockDetails;
            return textBlockEntity;
        }

        private static DateTimeBlockEntity CreateDateTimeBlockEntity()
        {
            var dateTimeBlockEntity = new DateTimeBlockEntity
            {
                Caption = "DateTimeBlockEntity1",
                Height = 99,
                Id = 88,
                Left = 77,
                Scene = null,
                SceneId = null,
                Top = 66,
                Width = 55,
                ZIndex = 44
            };
            var dateTimeBlockDetailsEntity = new DateTimeBlockDetailsEntity
            {
                Align = 1,
                BackColor = "aaa",
                Bold = 1,
                DatetimeBlockEntity = dateTimeBlockEntity,
                FontIndex = 1.1M,
                FontName = "Arial",
                FontSize = 14,
                Id = 1111,
                Italic = 1,
                TextColor = "ccc"
            };
            dateTimeBlockEntity.DateTimeBlockDetails = dateTimeBlockDetailsEntity;
            var dateTimeFormatEntity = new DateTimeFormatEntity
            {
                DateFormatFlag = 1,
                DatetTimeBlockDetailsEntities = new List<DateTimeBlockDetailsEntity> { dateTimeBlockDetailsEntity },
                Denomination = "DDD",
                DesigntimeFormat = "ddd",
                Id = 1,
                ShowtimeFormat = "ddd"
            };
            dateTimeBlockDetailsEntity.DateTimeFormatEntity = dateTimeFormatEntity;
            dateTimeBlockDetailsEntity.DateTimeFormatId = dateTimeFormatEntity.Id;
            return dateTimeBlockEntity;
        }

        private void InitGetter<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            _context.Get(Arg.Any<Expression<Func<TEntity, bool>>>())
                .Returns(entities.AsQueryable());
        }
    }
}

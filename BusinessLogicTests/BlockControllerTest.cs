﻿using AutoFixture;
using AutoFixture.Kernel;
using DomainObjects;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using ExpectedObjects;
using NSubstitute;
using NUnit.Framework;
using ServiceInterfaces;
using ServiceInterfaces.IRepositories;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTests
{
    [TestFixture]
    public class BlockControllerTest
    {
        private ISystemController _systemController;
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;
        private IRepository<DisplayBlock> _displayBlockRepository;
        private Fixture _fixture;

        [SetUp]
        public void BlockControllerTestSetup()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customizations.Add(new TypeRelay(typeof(DisplayBlock), typeof(TextBlock)));
            _fixture.Customizations.Add(new TypeRelay(typeof(DisplayBlock), typeof(TableBlock)));
            _fixture.Customizations.Add(new TypeRelay(typeof(DisplayBlock), typeof(PictureBlock)));
            _fixture.Customizations.Add(new TypeRelay(typeof(DisplayBlock), typeof(DateTimeBlock)));

            _systemController = Substitute.For<ISystemController>();

            _displayBlockRepository = Substitute.For<IDisplayBlockRepository>();

            _unitOfWork = Substitute.For<IUnitOfWork>();
            _unitOfWork.DisplayBlocks.Returns(_displayBlockRepository);

            _unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            _unitOfWorkFactory.Create().Returns(_unitOfWork);
        }

        [Test]
        public void SaveExistMetablockWithExistsFramesTest()
        {
            var controller = new BlockController(_systemController, _unitOfWorkFactory);

            var savingScenario = _fixture
                .Build<Scenario>()
                .Without(m => m.Scene)
                .Without(m => m.SceneId)
                .Create();

            var dbMetablock = new Scenario
            {
                Id = savingScenario.Id,
                Details = new ScenarioDetails
                {
                    Id = savingScenario.Details.Id,
                    Scenes = new List<Scene>(savingScenario.Details.Scenes.Select(f => new Scene
                    {
                        Id = f.Id,
                        Blocks = new List<DisplayBlock>()
                    }))
                }
            };

            _displayBlockRepository.GetById(Arg.Any<int>()).Returns(dbMetablock);

            var actual = controller.SaveScenario(savingScenario);
        }

        [Test]
        public void SaveExistMetablockWithNoExistsFramesTest()
        {
            var controller = new BlockController(_systemController, _unitOfWorkFactory);

            var savingMetablock = _fixture
                .Build<Scenario>()
                .Without(m => m.Scene)
                .Without(m => m.SceneId)
                .Create();

            var dbMetablock = new Scenario
            {
                Id = savingMetablock.Id,
                Details = new ScenarioDetails
                {
                    Id = savingMetablock.Details.Id,
                    Scenes = new List<Scene>()
                }
            };

            _displayBlockRepository.GetById(Arg.Any<int>()).Returns(dbMetablock);

            var actual = controller.SaveScenario(savingMetablock);
        }

        [Test]
        public void SaveDateTableBlock()
        {
            var controller = new BlockController(_systemController, _unitOfWorkFactory);

            var tableBlock = new TableBlock
            {
                Caption = "Caption",
                Height = 14,
                Top = 15,
                Left = 16,
                Width = 17,
                ZIndex = 0,
                Scene = null,
                SceneId = null,
            };
            var details = new TableBlockDetails
            {
                FontIndex = 1,
                FontName = "Font",
                FontSize = 15,
                Cells = new List<TableBlockCellDetails>(),
                TableBlockRowHeights = new List<TableBlockRowHeight>(),
                TableBlockColumnWidths = new List<TableBlockColumnWidth>(),
                HeaderDetails = new TableBlockHeaderDetails
                {
                    Align = Align.Center,
                    BackColor = "#ffffff",
                    Bold = false,
                    Italic = false,
                    TextColor = "#aaaaaa",
                },
                EvenRowDetails = new TableBlockEvenRowDetails
                {
                    Align = Align.Center,
                    BackColor = "#ffffff",
                    Bold = false,
                    Italic = false,
                    TextColor = "#aaaaaa",
                },
                OddRowDetails = new TableBlockOddRowDetails
                {
                    Align = Align.Center,
                    BackColor = "#ffffff",
                    Bold = false,
                    Italic = false,
                    TextColor = "#aaaaaa",
                }
            };
            tableBlock.Details = details;
            var rows = 3;
            var columns = 4;
            for (int row = 0; row < rows; row++)
                for (int column = 0; column < columns; column++)
                {
                    details.Cells.Add(new TableBlockCellDetails
                    {
                        Column = column,
                        Row = row,
                        TableBlockDetails = details,
                        Value = $"C[{row}][{column}]"
                    });
                }
            for (int row = 0; row < rows; row++)
            {
                details.TableBlockRowHeights.Add(new TableBlockRowHeight
                {
                    Index = row,
                    Value = row * 5,
                    Units = SizeUnits.Pecent,
                    TableBlockDetails = details,
                });
            }
            for (int column = 0; column < columns; column++)
            {
                details.TableBlockColumnWidths.Add(new TableBlockColumnWidth
                {
                    Index = column,
                    Value = null,
                    Units = SizeUnits.Auto,
                    TableBlockDetails = details,
                });
            }

            //_unitOfWork.DisplayBlocks.GetById(Arg.Any<int>()).Returns(new TableBlock
            //{
            //    Details = new TableBlockDetails
            //    {
            //        Cells = new List<TableBlockCellDetails>(),
            //        EvenRowDetails = new TableBlockRowDetails(),
            //        HeaderDetails = new TableBlockRowDetails(),
            //        OddRowDetails = new TableBlockRowDetails(),
            //        TableBlockColumnWidths = new List<TableBlockColumnWidth>(),
            //        TableBlockRowHeights = new List<TableBlockRowHeight>()
            //    }
            //});

            controller.SaveTableBlock(tableBlock);
        }
    }
}

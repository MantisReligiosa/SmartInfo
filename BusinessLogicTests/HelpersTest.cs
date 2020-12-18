using DomainObjects.Blocks.Details;
using Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BusinessLogicTests
{
    [TestFixture]
    public class HelpersTest
    {
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 1)]
        public void TestNoConditions(int currentIndex, int expectedNextIndex)
        {
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene>
                {
                    new Scene{Index = 1, Duration = 1},
                    new Scene{Index = 2, Duration = 1},
                    new Scene{Index = 3, Duration = 1}
                }
            };
            var nextFrame = helper.GetNextScene(new DateTime(), currentIndex);
            Assert.AreEqual(expectedNextIndex, nextFrame.Index);
        }

        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(4, 1)]
        public void TestTimeInterval(int currentIndex, int expectedNextIndex)
        {
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene>
                {
                    new Scene{Index = 1, UseInTimeInterval = true, UseFromTime = new TimeSpan(10,0,0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 2, UseInTimeInterval = true, UseFromTime = new TimeSpan(10, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 3, UseInTimeInterval = true, UseFromTime = new TimeSpan(19, 0, 0), UseToTime = new TimeSpan(23, 0, 0), Duration = 1},
                    new Scene{Index = 4, UseInTimeInterval = true, UseFromTime = new TimeSpan(10, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1}
                }
            };
            var nextFrame = helper.GetNextScene(new DateTime(1, 1, 1, 11, 0, 0), currentIndex);
            Assert.AreEqual(expectedNextIndex, nextFrame.Index);
        }

        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(4, 1)]
        public void TestDate(int currentIndex, int expectedNextIndex)
        {
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene>
                {
                    new Scene{Index = 1, UseInDate = true, DateToUse = new DateTime(1, 1, 3, 10, 0, 0), Duration = 1},
                    new Scene{Index = 2, UseInDate = true, DateToUse = new DateTime(1, 1, 3, 1, 0, 0), Duration = 1},
                    new Scene{Index = 3, UseInDate = true, DateToUse = new DateTime(1, 1, 4, 19, 0, 0), Duration = 1},
                    new Scene{Index = 4, UseInDate = true, DateToUse = new DateTime(1, 1, 3, 0, 0, 0), Duration = 1}
                }
            };
            var nextFrame = helper.GetNextScene(new DateTime(1, 1, 3, 11, 0, 0), currentIndex);
            Assert.AreEqual(expectedNextIndex, nextFrame.Index);
        }

        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(4, 1)]
        public void TestDayOfWeek(int currentIndex, int expectedNextIndex)
        {
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene>
                {
                    new Scene{Index = 1, UseInDayOfWeek = true, UseInWed = true, Duration = 1},
                    new Scene{Index = 2, UseInDayOfWeek = true, UseInWed = true, Duration = 1},
                    new Scene{Index = 3, UseInDayOfWeek = true, Duration = 1 },
                    new Scene{Index = 4, UseInDayOfWeek = true, UseInWed = true, Duration = 1}
                }
            };
            var nextFrame = helper.GetNextScene(new DateTime(1, 1, 3, 11, 0, 0), currentIndex);
            Assert.AreEqual(expectedNextIndex, nextFrame.Index);
        }

        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(3, 5)]
        [TestCase(4, 5)]
        [TestCase(5, 1)]
        public void TestSundayMorning(int currentIndex, int expectedNextIndex)
        {
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene>
                {
                    new Scene{Index = 1, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 2, UseInDayOfWeek = true, UseInWed = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 3, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 4, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(18, 0, 0), UseToTime = new TimeSpan(22, 0, 0), Duration = 1},
                    new Scene{Index = 5, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                }
            };
            var nextFrame = helper.GetNextScene(new DateTime(1, 1, 7, 10, 0, 0), currentIndex);
            Assert.AreEqual(expectedNextIndex, nextFrame.Index);
        }

        [Test]
        public void TestNull()
        {
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene>
                {
                    new Scene{Index = 1, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 2, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 3, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                    new Scene{Index = 4, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(18, 0, 0), UseToTime = new TimeSpan(22, 0, 0), Duration = 1},
                    new Scene{Index = 5, UseInDayOfWeek = true, UseInSun = true, UseInTimeInterval = true, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(12, 0, 0), Duration = 1},
                }
            };
            var nextFrame = helper.GetNextScene(new DateTime(1, 1, 1, 10, 0, 0), 1);
            Assert.Null(nextFrame);
        }

        [Test]
        public void TestGoToFirst()
        {
            var date = new DateTime(2020, 1, 1);
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene>
                {
                    new Scene{Index = 1, UseInDate = true, DateToUse = date, Duration = 1},
                    new Scene{Index = 2, Duration = 1 },
                    new Scene{Index = 3, UseInDate = true, DateToUse = date, Duration = 1},
                    new Scene{Index = 4, Duration = 1 },
                    new Scene{Index = 5, UseInDate = true, DateToUse = date, Duration = 1}
                }
            };

            var nextFrame = helper.GetNextScene(date, 5);
            Assert.AreEqual(1, nextFrame.Index);
        }

        [Test]
        public void TestNoUseInTime()
        {
            var date = new DateTime(2020, 09, 23);
            var firstFrame = new Scene { Index = 1, UseInTimeInterval = false, UseFromTime = new TimeSpan(8, 0, 0), UseToTime = new TimeSpan(10, 0, 0), Duration = 1 };
            var secondFrame = new Scene { Index = 2, UseInTimeInterval = false, UseFromTime = new TimeSpan(12, 0, 0), UseToTime = new TimeSpan(13, 0, 0), Duration = 1 };
            var helper = new ScenarioScheduler
            {
                Scenes = new List<Scene> { firstFrame, secondFrame }
            };
            var nextFrame = helper.GetNextScene(date, int.MinValue);
            Assert.AreEqual(firstFrame.Index, nextFrame.Index);
            nextFrame = helper.GetNextScene(date, firstFrame.Index);
            Assert.AreEqual(secondFrame.Index, nextFrame.Index);
            nextFrame = helper.GetNextScene(date, secondFrame.Index);
            Assert.AreEqual(firstFrame.Index, nextFrame.Index);
        }
    }
}

using DomainObjects.Parameters;
using NSubstitute;
using NUnit.Framework;
using Repository.Entities;
using Repository.Entities.ParameterEntities;
using Repository.Repositories;
using ServiceInterfaces;
using System;
using System.Linq.Expressions;

namespace RepositoryTests
{
    [TestFixture]
    public class ParameterRepositoryTest
    {
        private IDatabaseContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IDatabaseContext>();
        }

        [Test]
        public void GetScreenHeightTest()
        {
            var repository = new ParameterRepository(_context);

            var screenHeightEntity = new ScreenHeightEntity { Id = 12, Value = "123" };
            _context.SingleOrDefault(Arg.Any<Expression<Func<ParameterEntity, bool>>>()).Returns(screenHeightEntity);

            var height = repository.ScreenHeight;
            Assert.AreEqual(screenHeightEntity.Value, height.Value);
            Assert.AreEqual(screenHeightEntity.Id, height.Id);
            //var width = repository.ScreenWidth;
            //var color = repository.BackgroundColor;
        }

        [Test]
        public void GetScreenWidthTest()
        {
            var repository = new ParameterRepository(_context);

            var screenWidthEntity = new ScreenWidthEntity { Id = 12, Value = "123" };
            _context.SingleOrDefault(Arg.Any<Expression<Func<ParameterEntity, bool>>>()).Returns(screenWidthEntity);

            var width = repository.ScreenWidth;
            Assert.AreEqual(screenWidthEntity.Value, width.Value);
            Assert.AreEqual(screenWidthEntity.Id, width.Id);
        }

        [Test]
        public void GetBackgroundColorTest()
        {
            var repository = new ParameterRepository(_context);

            var backgroundColor = new BackgroundColorEntity { Id = 12, Value = "123" };
            _context.SingleOrDefault(Arg.Any<Expression<Func<ParameterEntity, bool>>>()).Returns(backgroundColor);

            var width = repository.BackgroundColor;
            Assert.AreEqual(backgroundColor.Value, width.Value);
            Assert.AreEqual(backgroundColor.Id, width.Id);
        }

        [Test]
        public void SetParametersTest()
        {
            var repository = new ParameterRepository(_context);

            var height = new ScreenHeight();
            var width = new ScreenWidth();
            var color = new BackgroundColor();

            repository.Create(height);
            repository.Create(width);
            repository.Create(color);
        }
    }
}

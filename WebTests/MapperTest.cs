using NUnit.Framework;
using Web.Profiles;

namespace WebTests
{
    [TestFixture]
    public class MapperTest
    {
        [Test]
        public void CreationTesting()
        {
            Assert.DoesNotThrow(() =>
            {
                var mapper = AutoMapperConfig.Mapper;
            });
        }
    }
}

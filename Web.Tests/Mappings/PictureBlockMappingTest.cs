using AutoFixture;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Web.Models.Blocks;
using Web.Profiles;
using Xunit;

namespace Web.Tests.Mappings
{
    public class PictureBlockMappingTest : IClassFixture<MapFixture<PictureBlockProfile>>
    {
        private readonly MapFixture<PictureBlockProfile> _mapFixture;
        private readonly Fixture _fixture;
        public PictureBlockMappingTest(MapFixture<PictureBlockProfile> mapFixture)
        {
            _mapFixture = mapFixture;
            _fixture = new Fixture();
        }

        [Fact]
        public void PictureBlockToDto()
        {
            var pictureBlock = _fixture.Build<PictureBlock>().Without(p => p.MetablockFrame).Without(p => p.MetablockFrameId).Create();
            var pictureBlockDto = _mapFixture.GetMapper().Map<PictureBlockDto>(pictureBlock);
            Assert.Equal(pictureBlock.Details.Base64Image, pictureBlockDto.Base64Src);
            Assert.Equal((int)pictureBlock.Details.ImageMode, pictureBlockDto.ImageMode);
            Assert.Equal(pictureBlock.Details.SaveProportions, pictureBlockDto.SaveProportions);
        }

        [Fact]
        public void DtoToPictureBlock()
        {
            var pictureBlockDto = _fixture.Build<PictureBlockDto>().Without(p => p.MetablockFrameId).Create();
            var pictureBlock =  _mapFixture.GetMapper().Map<PictureBlock>(pictureBlockDto);

            Assert.Equal(pictureBlockDto.Base64Src, pictureBlock.Details.Base64Image);
            Assert.Equal((ImageMode)pictureBlockDto.ImageMode, pictureBlock.Details.ImageMode);
            Assert.Equal(pictureBlockDto.SaveProportions, pictureBlock.Details.SaveProportions);
        }
    }
}

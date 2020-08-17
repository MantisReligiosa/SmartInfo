using AutoMapper;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Web.Models.Blocks;

namespace Web.Profiles
{
    public class PictureBlockProfile : Profile
    {
        public PictureBlockProfile()
        {
            CreateMap<PictureBlock, PictureBlockDto>()
                .ForMember(b => b.Type, opt => opt.MapFrom(b => BlockType.Picture))
                .ForMember(b => b.Base64Src, opt => opt.MapFrom(b => b.Details.Base64Image))
                .ForMember(b => b.Caption, opt => opt.MapFrom(b => string.IsNullOrEmpty(b.Caption) ? "picture" : b.Caption))
                .ForMember(b => b.ImageMode, opt => opt.MapFrom(b => (int)b.Details.ImageMode))
                .ForMember(b => b.SaveProportions, opt => opt.MapFrom(b => b.Details.SaveProportions));

            CreateMap<PictureBlockDto, PictureBlock>()
                .ForMember(b => b.Details, opt => opt.MapFrom(b => new PictureBlockDetails
                {
                    Base64Image = b.Base64Src,
                    ImageMode = (ImageMode)b.ImageMode,
                    SaveProportions = b.SaveProportions
                }));
        }
    }
}
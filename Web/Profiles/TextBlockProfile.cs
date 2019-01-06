using AutoMapper;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Web.Models.Blocks;

namespace Web.Profiles
{
    public class TextBlockProfile : Profile
    {
        public TextBlockProfile()
        {
            CreateMap<TextBlock, TextBlockDto>()
                .ForMember(b => b.Type, opt => opt.MapFrom(b => "text"))
                .ForMember(b => b.Text, opt => opt.MapFrom(b => b.Details.Text))
                .ForMember(b => b.BackColor, opt => opt.MapFrom(b => b.Details.BackColor))
                .ForMember(b => b.TextColor, opt => opt.MapFrom(b => b.Details.TextColor))
                .ForMember(b => b.Font, opt => opt.MapFrom(b => b.Details.FontName))
                .ForMember(b => b.FontSize, opt => opt.MapFrom(b => b.Details.FontSize));

            CreateMap<TextBlockDto, TextBlock>()
                .ForMember(b => b.Details, opt => opt.MapFrom(b => new TextBlockDetails
                {
                    Text = b.Text,
                    BackColor = b.BackColor,
                    TextColor = b.TextColor,
                    FontName = b.Font,
                    FontSize = b.FontSize,
                    Align = b.Align,
                    Italic = b.Italic,
                    Bold = b.Bold
                }));
        }
    }
}
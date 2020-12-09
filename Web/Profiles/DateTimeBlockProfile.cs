using AutoMapper;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Web.Models.Blocks;
using Web.Models.Blocks.Converter;

namespace Web.Profiles
{
    public class DateTimeBlockProfile : Profile
    {
        public DateTimeBlockProfile()
        {
            CreateMap<DateTimeBlock, DateTimeBlockDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => BlockIdProcessor.GetIdDTO(BlockType.Datetime, model.Id)))
                .ForMember(dto => dto.Type, opt => opt.MapFrom(model => BlockType.Datetime))
                .ForMember(dto => dto.BackColor, opt => opt.MapFrom(model => model.Details.BackColor))
                .ForMember(dto => dto.TextColor, opt => opt.MapFrom(model => model.Details.TextColor))
                .ForMember(dto => dto.Font, opt => opt.MapFrom(model => model.Details.FontName))
                .ForMember(dto => dto.FontSize, opt => opt.MapFrom(model => model.Details.FontSize))
                .ForMember(dto => dto.FontIndex, opt => opt.MapFrom(model => model.Details.FontIndex))
                .ForMember(dto => dto.Format, opt => opt.MapFrom(model => model.Details.Format))
                .ForMember(dto => dto.Align, opt => opt.MapFrom(model => model.Details.Align))
                .ForMember(dto => dto.Italic, opt => opt.MapFrom(model => model.Details.Italic))
                .ForMember(dto => dto.Bold, opt => opt.MapFrom(model => model.Details.Bold))
                .ForMember(dto => dto.Caption, opt => opt.MapFrom(model => string.IsNullOrEmpty(model.Caption) ? "datetime" : model.Caption))
                .ForMember(dto => dto.MetablockFrameId, opt => opt.MapFrom(model => model.SceneId));

            CreateMap<DateTimeBlockDto, DateTimeBlock>()
                .ForMember(model => model.Details, opt => opt.MapFrom(dto => new DateTimeBlockDetails
                {
                    BackColor = dto.BackColor,
                    TextColor = dto.TextColor,
                    FontName = dto.Font,
                    FontSize = dto.FontSize,
                    FontIndex = dto.FontIndex,
                    Align = dto.Align,
                    Italic = dto.Italic,
                    Bold = dto.Bold,
                    Format = dto.Format,
                }))
                .ForMember(model => model.SceneId, opt => opt.MapFrom(dto => dto.MetablockFrameId))
                .ForMember(model => model.Scene, opt => opt.Ignore());
        }
    }
}
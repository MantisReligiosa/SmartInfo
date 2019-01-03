using AutoMapper;
using DomainObjects.Blocks;
using System.Linq;
using Web.Models.Blocks;

namespace Web.Profiles
{
    public class TableBlockProfile : Profile
    {
        public TableBlockProfile()
        {
            CreateMap<TableBlock, TableBlockDto>()
                .ForMember(b => b.Type, opt => opt.MapFrom(b => "table"))
                .ForMember(b => b.Font, opt => opt.MapFrom(b => b.Details.FontName))
                .ForMember(b => b.FontSize, opt => opt.MapFrom(b => b.Details.FontSize))
                .ForMember(b => b.HeaderStyle, opt => opt.MapFrom(b => b.Details.HeaderDetails))
                .ForMember(b => b.EvenStyle, opt => opt.MapFrom(b => b.Details.EvenRowDetails))
                .ForMember(b => b.OddStyle, opt => opt.MapFrom(b => b.Details.OddRowDetails))
                .ForMember(b => b.Header, opt => opt.MapFrom(b => b.Details.Cells.Where(c => c.Row == b.Details.Cells.Min(cell => cell.Row)).OrderBy(c => c.Column).Select(c => c.Value).ToArray()))
                .ForMember(b => b.Rows, opt => opt.MapFrom(b => b.Details.Cells.Where(c => c.Row != b.Details.Cells.Min(cell => cell.Row)).OrderBy(c => c.Row).GroupBy(c => c.Row).Select(g=>g.OrderBy(c=>c.Column).Select(c=>c.Value).ToArray()).ToArray()));
        }
    }
}
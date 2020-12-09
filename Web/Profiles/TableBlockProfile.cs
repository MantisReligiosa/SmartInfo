using AutoMapper;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Blocks;
using Web.Models.Blocks.Converter;

namespace Web.Profiles
{
    public class TableBlockProfile : Profile
    {
        public TableBlockProfile()
        {
            CreateMap<TableBlock, TableBlockDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => BlockIdProcessor.GetIdDTO(BlockType.Table, model.Id)))
                .ForMember(b => b.Type, opt => opt.MapFrom(b => BlockType.Table))
                .ForMember(b => b.Font, opt => opt.MapFrom(b => b.Details.FontName))
                .ForMember(b => b.FontSize, opt => opt.MapFrom(b => b.Details.FontSize))
                .ForMember(b => b.FontIndex, opt => opt.MapFrom(b => b.Details.FontIndex))
                .ForMember(b => b.HeaderStyle, opt => opt.MapFrom(b => b.Details.HeaderDetails))
                .ForMember(b => b.EvenStyle, opt => opt.MapFrom(b => b.Details.EvenRowDetails))
                .ForMember(b => b.OddStyle, opt => opt.MapFrom(b => b.Details.OddRowDetails))
                .ForMember(b => b.Header, opt => opt.MapFrom(b => b.Details.Cells.Where(c => c.Row == b.Details.Cells.Min(cell => cell.Row)).OrderBy(c => c.Column).Select(c => c.Value).ToArray()))
                .ForMember(b => b.Rows, opt => opt.MapFrom(b => b.Details.Cells.Where(c => c.Row != b.Details.Cells.Min(cell => cell.Row)).OrderBy(c => c.Row).GroupBy(c => c.Row).Select(g =>
                new RowDto
                {
                    Index = g.Key - b.Details.Cells.Min(cell => cell.Row) - 1,
                    Cells = g.OrderBy(c => c.Column).Select(c => c.Value).ToArray()
                }
                ).ToArray()))
                .ForMember(b => b.Caption, opt => opt.MapFrom(b => string.IsNullOrEmpty(b.Caption) ? "table" : b.Caption))
                .ForMember(b => b.ColumnWidths, opt => opt.MapFrom(b => b.Details.TableBlockColumnWidths))
                .ForMember(b => b.RowHeights, opt => opt.MapFrom(b => b.Details.TableBlockRowHeights))
                .AfterMap((block, dto) =>
                {
                    if (!dto.ColumnWidths.Any())
                    {
                        var columns = new List<TableBlockColumnWidthDto>();
                        for (int i = 0; i < dto.Header.Length; i++)
                        {
                            columns.Add(new TableBlockColumnWidthDto
                            {
                                Index = i,
                                Units = DomainObjects.SizeUnits.Auto
                            });
                            dto.ColumnWidths = columns.ToArray();
                        }
                    }
                    if (!dto.RowHeights.Any())
                    {
                        var rows = new List<TableBlockRowHeightDto>();
                        for (int i = 0; i < dto.Rows.Length; i++)
                        {
                            rows.Add(new TableBlockRowHeightDto
                            {
                                Index = i,
                                Units = DomainObjects.SizeUnits.Auto
                            });
                            dto.RowHeights = rows.ToArray();
                        }
                    }
                });

            CreateMap<TableBlockDto, TableBlock>()
                .ForMember(b => b.Details, opt => opt.MapFrom(b => b));

            CreateMap<TableBlockDto, TableBlockDetails>()
                .ForMember(b => b.Id, opt => opt.Ignore())
                .ForMember(b => b.FontName, opt => opt.MapFrom(b => b.Font))
                .ForMember(b => b.EvenRowDetails, opt => opt.MapFrom(b => b.EvenStyle))
                .ForMember(b => b.OddRowDetails, opt => opt.MapFrom(b => b.OddStyle))
                .ForMember(b => b.HeaderDetails, opt => opt.MapFrom(b => b.HeaderStyle))
                .ForMember(b => b.TableBlockColumnWidths, opt => opt.MapFrom(b => b.ColumnWidths))
                .ForMember(b => b.TableBlockRowHeights, opt => opt.MapFrom(b => b.RowHeights))
                .ForMember(d => d.Cells, opt => opt.Ignore())
                .AfterMap((dto, details) =>
                {
                    var rowIndex = 0;
                    var columnIndex = 0;
                    details.Cells = new List<TableBlockCellDetails>();
                    foreach (var headerCellValue in dto.Header)
                    {
                        details.Cells.Add(new TableBlockCellDetails
                        {
                            Column = columnIndex++,
                            Row = rowIndex,
                            Value = headerCellValue,
                            TableBlockDetails = details
                        });
                    }
                    foreach (var row in dto.Rows)
                    {
                        rowIndex = row.Index + 1;
                        columnIndex = 0;
                        foreach (var cellValue in row.Cells)
                        {
                            details.Cells.Add(new TableBlockCellDetails
                            {
                                Column = columnIndex++,
                                Row = rowIndex,
                                Value = cellValue,
                                TableBlockDetails = details
                            });
                        }
                    }
                    foreach (var rowHeight in details.TableBlockRowHeights)
                    {
                        rowHeight.TableBlockDetails = details;
                    }
                    foreach (var columnWidth in details.TableBlockColumnWidths)
                    {
                        columnWidth.TableBlockDetails = details;
                    }
                });

            CreateMap<TableBlockRowDetails, RowStyleDto>();
            CreateMap<RowStyleDto, TableBlockEvenRowDetails>();
            CreateMap<RowStyleDto, TableBlockOddRowDetails>();
            CreateMap<RowStyleDto, TableBlockHeaderDetails>();
            CreateMap<TableBlockColumnWidth, TableBlockColumnWidthDto>();
            CreateMap<TableBlockColumnWidthDto, TableBlockColumnWidth>()
                .ForMember(model => model.TableBlockDetails, opt => opt.Ignore());
            CreateMap<TableBlockRowHeight, TableBlockRowHeightDto>();
            CreateMap<TableBlockRowHeightDto, TableBlockRowHeight>()
                .ForMember(model => model.TableBlockDetails, opt => opt.Ignore());

        }
    }
}
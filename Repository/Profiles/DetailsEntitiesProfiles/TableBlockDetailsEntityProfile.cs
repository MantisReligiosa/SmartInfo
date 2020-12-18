using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;
using Repository.Entities.DetailsEntities.TableBlockRowDetailsEntities;
using System.Linq;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class TableBlockDetailsEntityProfile : Profile
    {
        public TableBlockDetailsEntityProfile()
        {
            CreateMap<TableBlockDetailsEntity, TableBlockDetails>()
                .ForMember(model => model.HeaderDetails, opt => opt.MapFrom(entity => entity.RowDetailsEntities.OfType<TableBlockHeaderDetailsEntity>().Single()))
                .ForMember(model => model.EvenRowDetails, opt => opt.MapFrom(entity => entity.RowDetailsEntities.OfType<TableBlockEvenRowDetailsEntity>().Single()))
                .ForMember(model => model.OddRowDetails, opt => opt.MapFrom(entity => entity.RowDetailsEntities.OfType<TableBlockOddRowDetailsEntity>().Single()))
                .ForMember(model => model.Cells, opt => opt.MapFrom(entity => entity.CellDetailsEntities))
                .ForMember(model => model.TableBlockRowHeights, opt => opt.MapFrom(entity => entity.RowHeightsEntities))
                .ForMember(model => model.TableBlockColumnWidths, opt => opt.MapFrom(entity => entity.ColumnWidthEntities));

            CreateMap<TableBlockDetails, TableBlockDetailsEntity>()
                .ForMember(entity => entity.RowDetailsEntities, opt => opt.Ignore())
                .ForMember(entity => entity.CellDetailsEntities, opt => opt.Ignore())
                .ForMember(entity => entity.RowHeightsEntities, opt => opt.Ignore())
                .ForMember(entity => entity.ColumnWidthEntities, opt => opt.Ignore())
                .ForMember(entity => entity.TableBlockEntity, opt => opt.Ignore())
                .ForMember(entity => entity.Id, opt => opt.Ignore());
        }
    }
}

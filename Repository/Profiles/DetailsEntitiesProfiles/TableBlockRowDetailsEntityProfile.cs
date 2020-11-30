using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;
using Repository.Entities.DetailsEntities.TableBlockRowDetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class TableBlockRowDetailsEntityProfile : Profile
    {
        public TableBlockRowDetailsEntityProfile()
        {
            CreateMap<TableBlockRowDetailsEntity, TableBlockRowDetails>()
                .Include<TableBlockHeaderDetailsEntity, TableBlockHeaderDetails>()
                .Include<TableBlockEvenRowDetailsEntity, TableBlockEvenRowDetails>()
                .Include<TableBlockOddRowDetailsEntity, TableBlockOddRowDetails>();

            CreateMap<TableBlockRowDetails, TableBlockRowDetailsEntity>()
                .ForMember(entity => entity.TableBlockDetailsEntity, opt => opt.Ignore())
                .ForMember(entity => entity.TableBlockDetailsEntityId, opt => opt.Ignore())
                .Include<TableBlockHeaderDetails, TableBlockHeaderDetailsEntity>()
                .Include<TableBlockEvenRowDetails, TableBlockEvenRowDetailsEntity>()
                .Include<TableBlockOddRowDetails, TableBlockOddRowDetailsEntity>();

            CreateMap<TableBlockHeaderDetailsEntity, TableBlockHeaderDetails>();
            CreateMap<TableBlockEvenRowDetailsEntity, TableBlockEvenRowDetails>();
            CreateMap<TableBlockOddRowDetailsEntity, TableBlockOddRowDetails>();
            CreateMap<TableBlockHeaderDetails, TableBlockHeaderDetailsEntity>();
            CreateMap<TableBlockEvenRowDetails, TableBlockEvenRowDetailsEntity>();
            CreateMap<TableBlockOddRowDetails, TableBlockOddRowDetailsEntity>();
        }
    }
}

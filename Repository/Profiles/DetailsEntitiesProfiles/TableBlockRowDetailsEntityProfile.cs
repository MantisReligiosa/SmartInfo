using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class TableBlockRowDetailsEntityProfile : Profile
    {
        public TableBlockRowDetailsEntityProfile()
        {
            CreateMap<TableBlockRowDetailsEntity, TableBlockRowDetails>();

            CreateMap<TableBlockRowDetails, TableBlockRowDetailsEntity>()
                .ForMember(entity=>entity.TableBlockDetailsEntity,opt=>opt.Ignore())
                .ForMember(entity => entity.TableBlockDetailsEntityId, opt => opt.Ignore());
        }
    }
}

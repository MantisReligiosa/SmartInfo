using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class TableBlockRowHeightEntityProfile : Profile
    {
        public TableBlockRowHeightEntityProfile()
        {
            CreateMap<TableBlockRowHeightEntity, TableBlockRowHeight>()
                .ForMember(model => model.TableBlockDetails, opt => opt.MapFrom(entity => entity.TableBlockDetailsEntity));

            CreateMap<TableBlockRowHeight, TableBlockRowHeightEntity>()
                .ForMember(entity => entity.TableBlockDetailsEntity, opt => opt.MapFrom(model => model.TableBlockDetails))
                .ForMember(entity => entity.TableBlockDetailsEntityId, opt => opt.MapFrom(model => model.TableBlockDetails.Id));
        }
    }
}

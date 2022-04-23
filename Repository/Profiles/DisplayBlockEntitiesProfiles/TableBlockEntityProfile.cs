using AutoMapper;
using DomainObjects.Blocks;
using Repository.Entities.DisplayBlockEntities;

namespace Repository.Profiles.DisplayBlockEntitiesProfiles
{
    public class TableBlockEntityProfile : Profile
    {
        public TableBlockEntityProfile()
        {
            CreateMap<TableBlockEntity, TableBlock>()
                .ForMember(model => model.Details, opt => opt.MapFrom(entity => entity.TableBlockDetails));

            CreateMap<TableBlock, TableBlockEntity>()
                .ForMember(entity => entity.TableBlockDetails, opt => opt.MapFrom(model => model.Details))
                .AfterMap((model, entity) => entity.TableBlockDetails.TableBlockEntity = entity);
        }
    }
}

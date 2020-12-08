using AutoMapper;
using DomainObjects.Blocks.Details;
using Repository.Entities.DetailsEntities;

namespace Repository.Profiles.DetailsEntitiesProfiles
{
    public class TableBlockCellDetailsEntityProfile : Profile
    {
        public TableBlockCellDetailsEntityProfile()
        {
            CreateMap<TableBlockCellDetailsEntity, TableBlockCellDetails>()
                .ForMember(model => model.TableBlockDetails, opt => opt.MapFrom(entity => entity.TableBlockDetailsEntity));

            CreateMap<TableBlockCellDetails, TableBlockCellDetailsEntity>()
                .ForMember(entity => entity.TableBlockDetailsEntity, opt => opt.MapFrom(model => model.TableBlockDetails))
                .ForMember(entity => entity.TableBlockDetailsEntityId, opt => opt.MapFrom(model => model.TableBlockDetails.Id))
                .ForMember(entity => entity.Id, opt=>opt.Ignore());
        }
    }
}

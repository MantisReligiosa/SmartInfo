using DomainObjects.Blocks;
using Repository.Entities;
using Repository.Entities.DisplayBlockEntities;
using Repository.Specifications;
using ServiceInterfaces;
using ServiceInterfaces.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class DisplayBlockRepository : Repository<DisplayBlock, DisplayBlockEntity>, IDisplayBlockRepository
    {
        public DisplayBlockRepository(IDatabaseContext context)
            : base(context)
        {
        }

        public override void DeleteById(int id)
        {
            var displayBlock = Context.Find<DisplayBlockEntity>(id);

            switch (displayBlock)
            {
                case TextBlockEntity _:
                    DeleteBlockById<TextBlockEntity>(id);
                    return;
                case PictureBlockEntity _:
                    DeleteBlockById<PictureBlockEntity>(id);
                    return;
                case DateTimeBlockEntity _:
                    DeleteBlockById<DateTimeBlockEntity>(id);
                    return;
                case TableBlockEntity _:
                    DeleteBlockById<TableBlockEntity>(id);
                    return;
                case ScenarioEntity _:
                    DeleteBlockById<ScenarioEntity>(id);
                    return;
            }
        }

        private void DeleteBlockById<TBlock>(int id) where TBlock : DisplayBlockEntity
        {
            var entity = Context.GetWith(DisplayBlockSpecification.ById<TBlock>(id)).Single();
            Context.Remove(entity);
        }

        public IEnumerable<DisplayBlock> GetBlocksInScene(int? sceneId)
        {
            var entities = Context.Get(DisplayBlockSpecification.BySceneId<DisplayBlockEntity>(sceneId));
            var result = _mapper.Map<IEnumerable<DisplayBlock>>(entities);
            return result;
        }

        public IEnumerable<DisplayBlock> GetAllNonScenaried()
        {
            var textBlocksQuery = Context.Get(DisplayBlockSpecification.BySceneId<TextBlockEntity>(null));
            var pictureBlocksQuery = Context.Get(DisplayBlockSpecification.BySceneId<PictureBlockEntity>(null));
            var tableBlocksQuery = Context.Get(DisplayBlockSpecification.BySceneId<TableBlockEntity>(null));
            var dateTimeBlocksQuery = Context.Get(DisplayBlockSpecification.BySceneId<DateTimeBlockEntity>(null));
            var scenarioQuery = Context.Get(DisplayBlockSpecification.BySceneId<ScenarioEntity>(null));

            List<DisplayBlockEntity> entities = new List<DisplayBlockEntity>();
            entities.AddRange(textBlocksQuery);
            entities.AddRange(pictureBlocksQuery);
            entities.AddRange(dateTimeBlocksQuery);
            entities.AddRange(tableBlocksQuery);
            entities.AddRange(scenarioQuery);

            var result = _mapper.Map<IEnumerable<DisplayBlock>>(entities);
            return result;
        }

        public void DeleteAll()
        {
            Context.RemoveRange(Context.Get<DisplayBlockEntity>());
            Context.SaveChanges();
        }
    }
}

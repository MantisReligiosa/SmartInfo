using DomainObjects;
using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Repository.Entities;
using Repository.Entities.DetailsEntities;
using Repository.Entities.DisplayBlockEntities;
using Repository.Specifications;
using ServiceInterfaces;
using ServiceInterfaces.IRepositories;
using System;
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

        public override DisplayBlock Create(DisplayBlock item)
        {
            if (item is TableBlock tableBlock)
            {
                var entity = _mapper.Map<TableBlockEntity>(tableBlock);

                entity.TableBlockDetails.RowDetailsEntities = new List<TableBlockRowDetailsEntity>();

                var headerDetailsEntity = _mapper.Map<TableBlockRowDetailsEntity>(tableBlock.Details.HeaderDetails);
                headerDetailsEntity.TableBlockDetailsEntity = entity.TableBlockDetails;
                entity.TableBlockDetails.RowDetailsEntities.Add(headerDetailsEntity);

                var evenRowDetailsEntity = _mapper.Map<TableBlockRowDetailsEntity>(tableBlock.Details.EvenRowDetails);
                evenRowDetailsEntity.TableBlockDetailsEntity = entity.TableBlockDetails;
                entity.TableBlockDetails.RowDetailsEntities.Add(evenRowDetailsEntity);

                var oddRowDetailsEntity = _mapper.Map<TableBlockRowDetailsEntity>(tableBlock.Details.OddRowDetails);
                oddRowDetailsEntity.TableBlockDetailsEntity = entity.TableBlockDetails;
                entity.TableBlockDetails.RowDetailsEntities.Add(oddRowDetailsEntity);

                var addedEntity = Context.Add(entity);
                var result = _mapper.Map<TableBlockEntity, TableBlock>(addedEntity);
                return result;
            }
            return base.Create(item);
        }

        public override void Update(DisplayBlock item)
        {
            var displayBlock = Context.Find<DisplayBlockEntity>(item.Id);
            switch (displayBlock)
            {
                case TableBlockEntity tableBlockEntity:
                    var tableBlock = item as TableBlock;
                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details, entity => entity.RowDetailsEntities,
                        model => new List<TableBlockRowDetails> { model.EvenRowDetails, model.OddRowDetails, model.HeaderDetails },
                        (rowDetails, details) => rowDetails.TableBlockDetailsEntityId = details.Id);
                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details,
                        entity => entity.CellDetailsEntities,
                        model => model.Cells,
                        (cell, details) => cell.TableBlockDetailsEntityId = details.Id);
                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details,
                        entity => entity.ColumnWidthEntities,
                        model => model.TableBlockColumnWidths,
                        (columnWidth, details) => columnWidth.TableBlockDetailsEntityId = details.Id);
                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details,
                        entity => entity.RowHeightsEntities,
                        model => model.TableBlockRowHeights,
                        (rowHeight, details) => rowHeight.TableBlockDetailsEntityId = details.Id);
                    return;
                case ScenarioEntity scenarioEntity:
                    var scenario = item as Scenario;
                    UpdateCollection(scenarioEntity.ScenarioDetails, scenario.Details, entity => entity.Scenes, model => model.Scenes, (scene, details) => scene.ScenarioDetailsEntityId = details.Id);
                    return;
                default:
                    base.Update(item);
                    return;
            }

        }

        private void UpdateCollection<TEntity, TCollectionItemEntity, TModel, TCollectionItemModel>(
            TEntity entity,
            TModel model,
            Func<TEntity, ICollection<TCollectionItemEntity>> entityCollectionSelector,
            Func<TModel, IEnumerable<TCollectionItemModel>> modelCollectionSelector,
            Action<TCollectionItemEntity, TEntity> entityReferenceUpdateAction)
            where TCollectionItemEntity : Entity
            where TCollectionItemModel : Identity
        {
            var entityCollection = entityCollectionSelector(entity);
            var modelCollection = modelCollectionSelector(model);

            var itemsToDelete = entityCollection.Where(e => !modelCollection.Any(m => m.Id == e.Id));
            foreach (var itemToDelete in itemsToDelete)
            {
                entityCollection.Remove(itemToDelete);
            }

            var itemsToAdd = modelCollection.Where(m => !entityCollection.Any(e => m.Id == e.Id));
            foreach (var entityItemToAdd in _mapper.Map<IEnumerable<TCollectionItemModel>, IEnumerable<TCollectionItemEntity>>(itemsToAdd))
            {
                entityReferenceUpdateAction(entityItemToAdd, entity);

                entityCollection.Add(entityItemToAdd);
            }

            var itemsToUpdate = entityCollection.Where(e => modelCollection.Any(m => m.Id == e.Id));
            foreach (var entityItemToUpdate in itemsToUpdate)
            {
                var modelItemToUpdateFrom = modelCollection.Single(e => e.Id == entityItemToUpdate.Id);

                _mapper.Map(modelItemToUpdateFrom, entityItemToUpdate);
                entityReferenceUpdateAction(entityItemToUpdate, entity);
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

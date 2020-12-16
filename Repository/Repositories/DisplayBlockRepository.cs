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
            DisplayBlock result;
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
                Context.SaveChanges();
                result = _mapper.Map<TableBlockEntity, TableBlock>(addedEntity);
            }
            else if (item is Scenario scenario)
            {
                var entity = _mapper.Map<ScenarioEntity>(scenario);

                entity.ScenarioDetails.SceneEntities = new List<SceneEntity>();
                foreach (var scene in scenario.Details.Scenes)
                {
                    var sceneEntity = _mapper.Map<SceneEntity>(scene);
                    sceneEntity.ScenarioDetailsEntity = entity.ScenarioDetails;
                    entity.ScenarioDetails.SceneEntities.Add(sceneEntity);
                }

                var addedEntity = Context.Add(entity);
                Context.SaveChanges();
                result = _mapper.Map<ScenarioEntity, Scenario>(addedEntity);
            }
            else
            {
                result = base.Create(item);
            }
            return result;
        }

        public override void Update(DisplayBlock item)
        {
            var displayBlock = Context.Find<DisplayBlockEntity>(item.Id);
            var displayBlockSceneId = displayBlock.SceneId;
            var displayBlockScene = displayBlock.Scene;
            switch (displayBlock)
            {
                case TableBlockEntity tableBlockEntity:
                    var tableBlock = item as TableBlock;
                    base.Update(tableBlock);

                    if (tableBlockEntity.TableBlockDetails.RowDetailsEntities == null)
                    {
                        tableBlockEntity.TableBlockDetails.RowDetailsEntities = new List<TableBlockRowDetailsEntity>();
                    }
                    if (tableBlockEntity.TableBlockDetails.CellDetailsEntities == null)
                    {
                        tableBlockEntity.TableBlockDetails.CellDetailsEntities = new List<TableBlockCellDetailsEntity>();
                    }
                    if (tableBlockEntity.TableBlockDetails.ColumnWidthEntities == null)
                    {
                        tableBlockEntity.TableBlockDetails.ColumnWidthEntities = new List<TableBlockColumnWidthEntity>();
                    }
                    if (tableBlockEntity.TableBlockDetails.RowHeightsEntities == null)
                    {
                        tableBlockEntity.TableBlockDetails.RowHeightsEntities = new List<TableBlockRowHeightEntity>();
                    }

                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details, entity => entity.RowDetailsEntities,
                        model => new List<TableBlockRowDetails> { model.EvenRowDetails, model.OddRowDetails, model.HeaderDetails },
                        (rowDetails, details) => { rowDetails.TableBlockDetailsEntityId = details.Id; rowDetails.TableBlockDetailsEntity = details; },
                        (mRowDetail, eRowDetail) => mRowDetail.Id == eRowDetail.Id);
                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details,
                        entity => entity.CellDetailsEntities,
                        model => model.Cells,
                        (cell, details) => { cell.TableBlockDetailsEntityId = details.Id; cell.TableBlockDetailsEntity = details; },
                        (mCell, eCell) => mCell.Column == eCell.Column && mCell.Row == eCell.Row);
                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details,
                        entity => entity.ColumnWidthEntities,
                        model => model.TableBlockColumnWidths,
                        (columnWidth, details) => { columnWidth.TableBlockDetailsEntityId = details.Id; columnWidth.TableBlockDetailsEntity = details; },
                        (mColumn, eColumn) => mColumn.Index == eColumn.Index);
                    UpdateCollection(tableBlockEntity.TableBlockDetails,
                        tableBlock.Details,
                        entity => entity.RowHeightsEntities,
                        model => model.TableBlockRowHeights,
                        (rowHeight, details) => { rowHeight.TableBlockDetailsEntityId = details.Id; rowHeight.TableBlockDetailsEntity = details; },
                        (mRow, eRow) => mRow.Index == eRow.Index);
                    break;
                case ScenarioEntity scenarioEntity:
                    var scenario = item as Scenario;
                    base.Update(scenario);

                    if (scenarioEntity.ScenarioDetails.SceneEntities == null)
                    {
                        scenarioEntity.ScenarioDetails.SceneEntities = new List<SceneEntity>();
                    }

                    UpdateCollection(scenarioEntity.ScenarioDetails,
                        scenario.Details,
                        entity => entity.SceneEntities,
                        model => model.Scenes,
                        (sceneEntity, details) => 
                        { 
                            sceneEntity.ScenarioDetailsEntityId = details.Id; 
                            sceneEntity.ScenarioDetailsEntity = details;
                            if (sceneEntity.DisplayBlocks != null)
                            {
                                foreach (var sceneDisplayBlockEntity in sceneEntity.DisplayBlocks)
                                {
                                    sceneDisplayBlockEntity.Scene = sceneEntity;
                                    sceneDisplayBlockEntity.SceneId = sceneEntity.Id;
                                }
                            }
                        },
                        (mScene, eScene) => mScene.Id == eScene.Id);
                    break;
                default:
                    base.Update(item);
                    break;
            }
            if (displayBlockScene != null)
            {
                displayBlock.Scene = displayBlockScene;
                displayBlock.SceneId = displayBlockSceneId;
            }
            Context.SaveChanges();
        }

        private void UpdateCollection<TEntity, TCollectionItemEntity, TModel, TCollectionItemModel>(
            TEntity entity,
            TModel model,
            Func<TEntity, ICollection<TCollectionItemEntity>> entityCollectionSelector,
            Func<TModel, IEnumerable<TCollectionItemModel>> modelCollectionSelector,
            Action<TCollectionItemEntity, TEntity> entityReferenceUpdateAction,
            Func<TCollectionItemModel, TCollectionItemEntity, bool> equalPredicate)
            where TCollectionItemEntity : Entity
            where TCollectionItemModel : Identity
        {
            var entityCollection = entityCollectionSelector(entity);
            var modelCollection = modelCollectionSelector(model);

            var itemsToDelete = entityCollection.Where(e => !modelCollection.Any(m => equalPredicate(m, e))).ToList();
            foreach (var itemToDelete in itemsToDelete)
            {
                entityCollection.Remove(itemToDelete);
                Context.Remove(itemToDelete);
            }

            var itemsToUpdate = entityCollection.Where(e => modelCollection.Any(m => equalPredicate(m, e))).ToList();
            foreach (var entityItemToUpdate in itemsToUpdate)
            {
                var modelItemToUpdateFrom = modelCollection.Single(m => equalPredicate(m, entityItemToUpdate));

                _mapper.Map(modelItemToUpdateFrom, entityItemToUpdate);
                entityReferenceUpdateAction(entityItemToUpdate, entity);
            }

            var itemsToAdd = modelCollection.Where(m => !entityCollection.Any(e => equalPredicate(m, e))).ToList();
            foreach (var entityItemToAdd in _mapper.Map<IEnumerable<TCollectionItemModel>, IEnumerable<TCollectionItemEntity>>(itemsToAdd))
            {
                entityReferenceUpdateAction(entityItemToAdd, entity);

                entityCollection.Add(entityItemToAdd);
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

            var entities = new List<DisplayBlockEntity>();
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
        }
    }
}

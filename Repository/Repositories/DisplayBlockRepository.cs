using DomainObjects.Blocks;
using Repository.Entities;
using Repository.Entities.DisplayBlockEntities;
using Repository.Specifications;
using ServiceInterfaces;
using ServiceInterfaces.IRepositories;
using System.Collections.Generic;

namespace Repository.Repositories
{
    public class DisplayBlockRepository : Repository<DisplayBlock, DisplayBlockEntity>, IDisplayBlockRepository
    {
        public DisplayBlockRepository(IDatabaseContext context)
            : base(context)
        {
        }

        //public override IEnumerable<DisplayBlock> GetAll()
        //{
        //    var items = new List<DisplayBlock>();
        //    items.AddRange(Context.DisplayBlocks.OfType<TextBlock>().Include(t => t.Details).ToList());
        //    items.AddRange(Context.DisplayBlocks.OfType<PictureBlock>().Include(t => t.Details).ToList());
        //    items.AddRange(Context.DisplayBlocks.OfType<DateTimeBlock>().Include(t => t.Details).Include(t => t.Details.Format).ToList());
        //    items.AddRange(Context.DisplayBlocks.OfType<TableBlock>()
        //        .Include(t => t.Details)
        //        .Include(t => t.Details.Cells)
        //        .Include(t => t.Details.EvenRowDetails)
        //        .Include(t => t.Details.OddRowDetails)
        //        .Include(t => t.Details.HeaderDetails)
        //        .Include(t => t.Details.TableBlockColumnWidths)
        //        .Include(t => t.Details.TableBlockRowHeights)
        //        .ToList());
        //    items.AddRange(Context.DisplayBlocks.OfType<MetaBlock>()
        //        .Include(t => t.Details)
        //        .Include(t => t.Details.Frames)
        //        .ToList());

        //    return items;
        //}

        //public override void DeleteRange(IEnumerable<DisplayBlock> list)
        //{
        //    list.ToList().ForEach(l => DeleteById(l.Id));
        //}

        //public override void DeleteById(int id)
        //{
        //    var displayBlock = GetById(id);
        //    if (displayBlock is TextBlock textBlock && textBlock.Details != null)
        //    {
        //        Context.Set<TextBlockDetails>().Remove(textBlock.Details);
        //    }
        //    if (displayBlock is PictureBlock pictureBlock && pictureBlock.Details != null)
        //    {
        //        Context.Set<PictureBlockDetails>().Remove(pictureBlock.Details);
        //    }
        //    if (displayBlock is TableBlock tableBlock && tableBlock.Details != null)
        //    {
        //        var rowDetails = new List<TableBlockRowDetails>
        //            {
        //                tableBlock.Details.HeaderDetails,
        //                tableBlock.Details.EvenRowDetails,
        //                tableBlock.Details.OddRowDetails
        //            };
        //        Context.Set<TableBlockDetails>().Remove(tableBlock.Details);
        //        Context.Set<TableBlockRowDetails>().RemoveRange(rowDetails);
        //    }
        //    if (displayBlock is DateTimeBlock dateTimeBlock && dateTimeBlock.Details != null)
        //    {
        //        Context.Set<DateTimeBlockDetails>().Remove(dateTimeBlock.Details);
        //    }
        //    if (displayBlock is MetaBlock metaBlock && metaBlock.Details != null)
        //    {
        //        var innerBlockIds = metaBlock.Details.Frames.SelectMany(frame => frame.Blocks ?? new List<DisplayBlock>(), (frame, block) => block.Id).ToList();
        //        foreach (var innerBlockId in innerBlockIds)
        //        {
        //            DeleteById(innerBlockId);
        //        }
        //        Context.Set<MetablockFrame>().RemoveRange(metaBlock.Details.Frames);
        //        Context.Set<MetaBlockDetails>().Remove(metaBlock.Details);
        //    }
        //    base.DeleteById(id);
        //}

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

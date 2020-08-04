using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using Repository.QueuedTasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Repositories
{
    public class DisplayBlockRepository : CachedRepository<DisplayBlock>
    {
        public DisplayBlockRepository(DatabaseContext context)
            : base(context)
        {
        }

        public override IEnumerable<DisplayBlock> GetAll()
        {
            if (GetFullyCachedEneities().Contains(typeof(DisplayBlock)))
            {
                return GetCache().OfType<DisplayBlock>();
            }
            var items = new List<DisplayBlock>();
            items.AddRange(Context.DisplayBlocks.OfType<TextBlock>().Include(t => t.Details).ToList());
            items.AddRange(Context.DisplayBlocks.OfType<PictureBlock>().Include(t => t.Details).ToList());
            items.AddRange(Context.DisplayBlocks.OfType<DateTimeBlock>().Include(t => t.Details).Include(t => t.Details.Format).ToList());
            items.AddRange(Context.DisplayBlocks.OfType<TableBlock>()
                .Include(t => t.Details)
                .Include(t => t.Details.Cells)
                .Include(t => t.Details.EvenRowDetails)
                .Include(t => t.Details.OddRowDetails)
                .Include(t => t.Details.HeaderDetails)
                .ToList());
            items.AddRange(Context.DisplayBlocks.OfType<MetaBlock>()
                .Include(t=>t.Details)
                .Include(t=>t.Details.Frames)
                .ToList());

            GetCache().AddRange(items);
            GetFullyCachedEneities().Add(typeof(DisplayBlock));
            return items;
        }

        public override void Delete(Guid id)
        {
            var displayBlock = GetCache().OfType<DisplayBlock>().FirstOrDefault(i => i.Id.Equals(id));
            if (displayBlock != null)
                GetCache().Remove(displayBlock);
            var task = new GuidTask((identity) =>
            {
                if (displayBlock == null)
                {
                    displayBlock = base.Get(id);
                }
                base.Delete(identity);
                if (displayBlock is TextBlock textBlock)
                {
                    Context.Set<TextBlockDetails>().Remove(textBlock.Details);
                }
                else if (displayBlock is PictureBlock pictureBlock)
                {
                    Context.Set<PictureBlockDetails>().Remove(pictureBlock.Details);
                }
                else if (displayBlock is TableBlock tableBlock)
                {
                    var rowDetails = new List<TableBlockRowDetails>
                    {
                        tableBlock.Details.HeaderDetails,
                        tableBlock.Details.EvenRowDetails,
                        tableBlock.Details.OddRowDetails
                    };
                    Context.Set<TableBlockDetails>().Remove(tableBlock.Details);
                    Context.Set<TableBlockRowDetails>().RemoveRange(rowDetails);
                }
            }, id);
            GetTaskQueue().Enqueue(task);
        }
    }
}

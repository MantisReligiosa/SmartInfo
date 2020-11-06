using DomainObjects.Blocks;
using DomainObjects.Blocks.Details;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Repositories
{
    public class DisplayBlockRepository : Repository<DisplayBlock>
    {
        public DisplayBlockRepository(DatabaseContext context)
            : base(context)
        {
        }

        public override IEnumerable<DisplayBlock> GetAll()
        {
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
                .Include(t => t.Details.TableBlockColumnWidths)
                .Include(t => t.Details.TableBlockRowHeights)
                .ToList());
            items.AddRange(Context.DisplayBlocks.OfType<MetaBlock>()
                .Include(t => t.Details)
                .Include(t => t.Details.Frames)
                .ToList());

            return items;
        }


        public override void DeleteRange(IEnumerable<DisplayBlock> list)
        {
            list.ToList().ForEach(l => Delete(l.Id));
        }

        public override void Delete(Guid id)
        {
            var displayBlock = Get(id);
            if (displayBlock is TextBlock textBlock && textBlock.Details != null)
            {
                Context.Set<TextBlockDetails>().Remove(textBlock.Details);
            }
            if (displayBlock is PictureBlock pictureBlock && pictureBlock.Details != null)
            {
                Context.Set<PictureBlockDetails>().Remove(pictureBlock.Details);
            }
            if (displayBlock is TableBlock tableBlock && tableBlock.Details != null)
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
            if (displayBlock is DateTimeBlock dateTimeBlock && dateTimeBlock.Details != null)
            {
                Context.Set<DateTimeBlockDetails>().Remove(dateTimeBlock.Details);
            }
            if (displayBlock is MetaBlock metaBlock && metaBlock.Details != null)
            {
                var innerBlockIds = metaBlock.Details.Frames.SelectMany(frame => frame.Blocks ?? new List<DisplayBlock>(), (frame, block) => block.Id).ToList();
                foreach (var innerBlockId in innerBlockIds)
                {
                    Delete(innerBlockId);
                }
                Context.Set<MetablockFrame>().RemoveRange(metaBlock.Details.Frames);
                Context.Set<MetaBlockDetails>().Remove(metaBlock.Details);
            }
            base.Delete(id);
        }
    }
}

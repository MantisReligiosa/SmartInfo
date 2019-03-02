using DomainObjects.Blocks;
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
            items.AddRange(Context.DisplayBlocks.OfType<TableBlock>()
                .Include(t => t.Details)
                .Include(t => t.Details.Cells)
                .Include(t => t.Details.EvenRowDetails)
                .Include(t => t.Details.OddRowDetails)
                .Include(t => t.Details.HeaderDetails)
                .ToList());

            GetCache().AddRange(items);
            GetFullyCachedEneities().Add(typeof(DisplayBlock));
            return items;
        }
    }
}

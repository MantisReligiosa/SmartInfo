using DomainObjects.Blocks;
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
            var result = new List<DisplayBlock>();
            result.AddRange(Context.DisplayBlocks.OfType<TextBlock>().Include(t => t.Details).ToList());
            result.AddRange(Context.DisplayBlocks.OfType<PictureBlock>().Include(t => t.Details).ToList());
            result.AddRange(Context.DisplayBlocks.OfType<TableBlock>()
                .Include(t => t.Details)
                .Include(t => t.Details.Cells)
                .Include(t => t.Details.EvenRowDetails)
                .Include(t => t.Details.OddRowDetails)
                .Include(t => t.Details.HeaderDetails)
                .ToList());
            return result;
        }

    }
}

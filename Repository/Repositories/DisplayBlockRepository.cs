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
            return result;
        }

    }
}

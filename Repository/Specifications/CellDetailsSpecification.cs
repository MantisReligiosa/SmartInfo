using Repository.Entities.DetailsEntities;
using System;
using System.Linq.Expressions;

namespace Repository.Specifications
{
    public static class CellDetailsSpecification
    {
        internal static Expression<Func<TableBlockCellDetailsEntity, bool>> ByTableBlockDetailsId(int id)
        {
            return b => b.TableBlockDetailsEntityId == id;
        }
    }
}

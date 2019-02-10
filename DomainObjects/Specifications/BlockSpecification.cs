using DomainObjects.Blocks;
using System;
using System.Linq.Expressions;

namespace DomainObjects.Specifications
{
    public static class BlockSpecification
    {
        public static Expression<Func<DisplayBlock, bool>> OfType<T>() where T : DisplayBlock
        {
            return u => u is T;
        }
    }
}

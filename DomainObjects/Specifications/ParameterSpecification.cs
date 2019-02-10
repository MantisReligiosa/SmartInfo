using System;
using System.Linq.Expressions;

namespace DomainObjects.Specifications
{
    public static class ParameterSpecification
    {
        public static Expression<Func<Parameter, bool>> OfType<T>() where T : Parameter
        {
            return u => u is T;
        }
    }
}

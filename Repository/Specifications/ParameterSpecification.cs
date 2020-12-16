using Repository.Entities;
using System;
using System.Linq.Expressions;

namespace Repository.Specifications
{
    public static class ParameterSpecification
    {
        public static Expression<Func<ParameterEntity, bool>> OfType<T>() where T : ParameterEntity
        {
            return u => u is T;
        }
    }
}

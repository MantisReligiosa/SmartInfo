using System;
using System.Linq.Expressions;

namespace DomainObjects.Specifications
{
    public static class ParameterSpecification
    {
        public static Expression<Func<Parameter, bool>> ByName(string name)
        {
            return u => u.Name.Equals(name);
        }
    }
}

using System;
using System.Linq.Expressions;

namespace DomainObjects.Specifications
{
    public static class UserSpecification
    {
        public static Expression<Func<User, bool>> ByName(string name)
        {
            return u => u.Login.Equals(name);
        }
    }
}

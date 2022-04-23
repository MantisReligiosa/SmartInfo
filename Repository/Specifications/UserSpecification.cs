using Repository.Entities;
using System;
using System.Linq.Expressions;

namespace Repository.Specifications
{
    public static class UserSpecification
    {
        public static Expression<Func<UserEntity, bool>> ByName(string name)
        {
            return u => u.Login.Equals(name);
        }

        internal static Expression<Func<UserEntity, bool>> ByGuid(Guid identifier)
        {
            return u => u.GuidStr == identifier.ToString();
        }
    }
}

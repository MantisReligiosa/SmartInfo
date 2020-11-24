using DomainObjects;
using Repository.Entities;
using Repository.Specifications;
using ServiceInterfaces.IRepositories;
using System;
using System.Linq;

namespace Repository.Repositories
{
    public class UserRepository : Repository<User, UserEntity>, IUserRepository
    {
        public UserRepository(DatabaseContext context)
            : base(context)
        {
        }

        public User FindByGuid(Guid identifier)
        {
            var entity = Context.Set<UserEntity>().Single(UserSpecification.ByGuid(identifier));
            var result = _mapper.Map<User>(entity);
            return result;
        }

        public User FindByName(string login)
        {
            var entity = Context.Set<UserEntity>().Single(UserSpecification.ByName(login));
            var result =  _mapper.Map<User>(entity);
            return result;
        }
    }
}

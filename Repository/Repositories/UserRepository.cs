using DomainObjects;
using Repository.Entities;
using Repository.Specifications;
using ServiceInterfaces;
using ServiceInterfaces.IRepositories;
using System;

namespace Repository.Repositories
{
    public class UserRepository : Repository<User, UserEntity>, IUserRepository
    {
        public UserRepository(IDatabaseContext context)
            : base(context)
        {
        }

        public User FindByGuid(Guid identifier)
        {
            var entity = Context.Single(UserSpecification.ByGuid(identifier));
            var result = _mapper.Map<User>(entity);
            return result;
        }

        public User FindByName(string login)
        {
            var entity = Context.SingleOrDefault(UserSpecification.ByName(login));
            var result = _mapper.Map<User>(entity);
            return result;
        }
    }
}

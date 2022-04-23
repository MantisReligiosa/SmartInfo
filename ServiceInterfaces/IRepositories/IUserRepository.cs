using DomainObjects;
using System;

namespace ServiceInterfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByName(string login);
        User FindByGuid(Guid identifier);
    }
}

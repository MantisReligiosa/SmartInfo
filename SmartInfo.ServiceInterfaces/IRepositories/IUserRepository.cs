using DomainObjects;

namespace SmartInfo.ServiceInterfaces.IRepositories;

public interface IUserRepository : IRepository<User>
{
    User FindByName(string login);
    User FindByGuid(Guid identifier);
}
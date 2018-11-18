using DomainObjects;
using System;

namespace ServiceInterfaces
{
    public interface IAccountController
    {
        User GetUserByIdentifier(Guid identifier);
        User GetUserByName(string login);
    }
}

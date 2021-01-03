using DomainObjects;
using System;

namespace ServiceInterfaces
{
    public interface IAccountController
    {
        User GetUserByIdentifier(Guid identifier);
        User GetUserByName(string login);
        bool IsPasswordCorrect(User user, string password);
        void ChangePassword(Guid userId, string newLogin, string newPassword);
    }
}

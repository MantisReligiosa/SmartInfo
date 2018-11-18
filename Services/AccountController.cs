using DomainObjects;
using ServiceInterfaces;
using System;

namespace Services
{
    public class AccountController : IAccountController
    {
        private readonly User _user = new User
        {
            Identifier = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1),
            Login = "1"
        };

        public User GetUserByIdentifier(Guid identifier)
        {
            if (identifier.Equals(_user.Identifier))
                return _user;
            return null;
        }

        public User GetUserByName(string login)
        {
            if (login.Equals(_user.Login))
                return _user;
            return null;
        }

    }
}

using DomainObjects;
using ServiceInterfaces;
using System;
using System.Linq;

namespace Services
{
    public class AccountController : IAccountController
    {
        private readonly ICryptoProvider _cryptoProvider;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(ICryptoProvider cryptoProvider, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _cryptoProvider = cryptoProvider;
            _unitOfWork = unitOfWorkFactory.Create();
        }

        public void ChangePassword(Guid userId, string newLogin, string newPassword)
        {
            var user = _unitOfWork.Users.FindByGuid(userId);
            user.PasswordHash = _cryptoProvider.Hash(newPassword);
            if (!string.IsNullOrWhiteSpace(newLogin))
            {
                user.Login = newLogin;
            }
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
        }

        public User GetUserByIdentifier(Guid identifier)
        {
            var user = _unitOfWork.Users.FindByGuid(identifier);
            return user;
        }

        public User GetUserByName(string login)
        {
            var user = _unitOfWork.Users.FindByName(login);
            return user;
        }

        public bool IsPasswordCorrect(User user, string password)
        {
            return _cryptoProvider.Hash(password).Equals(user.PasswordHash);
        }

    }
}

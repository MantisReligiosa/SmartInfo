using DomainObjects;
using DomainObjects.Specifications;
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

        public User GetUserByIdentifier(Guid identifier)
        {
            var user = _unitOfWork.Users.Get(identifier);
            return user;
        }

        public User GetUserByName(string login)
        {
            var user = _unitOfWork.Users.Find(UserSpecification.ByName(login)).FirstOrDefault();
            return user;
        }

        public bool IsPasswordCorrect(User user, string password)
        {
            return _cryptoProvider.Hash(password).Equals(user.PasswordHash);
        }

    }
}

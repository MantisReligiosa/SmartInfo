﻿using DomainObjects;
using ServiceInterfaces;
using System;
using System.Text.RegularExpressions;

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

        public bool IsNewLoginValid(string newLogin) => string.IsNullOrEmpty(newLogin) || IsValid(newLogin);

        public bool IsNewPasswordValid(string newPassword) => !string.IsNullOrEmpty(newPassword) && IsValid(newPassword);

        private bool IsValid(string newPassword) => Regex.Matches(newPassword, $@"^[a-zA-Z0-9\~\`\!\@\#\$\%\^\&\*\(\)_\-\+\=\<\,\>\.\?\/\:\;\""\'\|\\\[\]\{{\}}]{{5,}}$").Count == 1;

        public bool IsPasswordCorrect(User user, string password)
        {
            return !string.IsNullOrEmpty(password) && _cryptoProvider.Hash(password).Equals(user.PasswordHash);
        }

    }
}

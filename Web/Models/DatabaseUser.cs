using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using ServiceInterfaces;
using System;

namespace Web.Models
{
    public class DatabaseUser : IUserMapper
    {
        private readonly IAccountController _accountController;

        public DatabaseUser(IAccountController accountController)
        {
            _accountController = accountController;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var user = _accountController.GetUserByIdentifier(identifier);

            if (user == null)
                return null;

            return new AuthenticatedUser
            {
                UserName = user.Login,
                Claims = new[]
                {
                    "NewUser",
                    "CanComment"
                }
            };
        }
    }
}
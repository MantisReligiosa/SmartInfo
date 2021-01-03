using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using ServiceInterfaces;
using Web.Models;

namespace Web.Modules
{
    public class AccountModule : NancyModule
    {
        private readonly IAccountController _accountController;

        public AccountModule(IAccountController accountController)
            : base()
        {
            _accountController = accountController;

            Post["/api/changePassword"] = parameters =>
            {
                var data = this.Bind<ChangeCreditsRequest>();
                var user = _accountController.GetUserByName(Context.CurrentUser.UserName);
                var accessGranted = user != null && _accountController.IsPasswordCorrect(user, data.Password);
                if (!accessGranted)
                {
                    return Response.AsJson(new ErrorResponce { ErrorMessage = "В доступе отказано" });
                }
                _accountController.ChangePassword(user.GUID, data.NewLogin, data.NewPassword);
                var result = this.LogoutAndRedirect("/");
                return result;
            };
        }
    }
}
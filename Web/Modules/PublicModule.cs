using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using ServiceInterfaces;
using Web.Models;

namespace Web.Modules
{
    public class PublicModule : NancyModule
    {
        public PublicModule(ISystemController systemController, IAccountController accountController)
        {
            Get["/login"] = parameters =>
            {
                return View["Home/Login.cshtml", new ViewModel { Version = systemController.GetVersion(), AccessGranted = true }];
            };
            Post["/login"] = parameters =>
            {
                var data = this.Bind<CreditsRequest>();
                var user = accountController.GetUserByName(data.Login);
                var accessGranted = user != null && accountController.IsPasswordCorrect(user, data.Password);
                var viewModel = new ViewModel
                {
                    Version = systemController.GetVersion(),
                    User = user,
                    AccessGranted = accessGranted
                };
                if (!accessGranted)
                {
                    return View["Home/Login.cshtml", viewModel];
                }

                var result = this.LoginAndRedirect(user.Id, null, "/");
                return result;
            };
            Post["/version"] = parameters =>
            {
                return systemController.GetVersion();
            };
        }
    }
}
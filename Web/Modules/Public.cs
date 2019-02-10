using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using ServiceInterfaces;
using Web.Models;


namespace Web.Modules
{
    public class Public : NancyModule
    {
        public Public(IAccountController accountController)
        {
            Get["/login"] = parameters =>
            {
                return View["Home/Login.cshtml"];
            };
            Post["/login"] = parameters =>
            {
                var data = this.Bind<CreditsRequest>();
                var user = accountController.GetUserByName(data.Login);
                if (user == null || !accountController.IsPasswordCorrect(user, data.Password))
                {
                    return View["Home/Login.cshtml", false];
                }

                var result = this.LoginAndRedirect(user.Id, null, "/");
                return result;
            };
        }
    }
}
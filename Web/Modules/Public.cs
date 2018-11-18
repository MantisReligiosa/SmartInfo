using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Linker;
using Nancy.ModelBinding;
using ServiceInterfaces;
using Web.Models;


namespace Web.Modules
{
    public class Public : NancyModule
    {
        public Public(IAccountController accountController, IResourceLinker linker)
        {
            Get["/login"] = parameters =>
            {
                return View["Home/Login.cshtml"];
            };
            Post["/login"] = parameters =>
            {
                var data = this.Bind<Credits>();
                var user = accountController.GetUserByName(data.Login);
                if (user == null || !user.IsPasswordCorrect(data.Password))
                {
#warning выкинуть тостер!!!!
#warning удалить login.viewmodel?
                    return View["Home/Login.cshtml", false];
                }

                var result = this.LoginAndRedirect(user.Identifier, null, "/");
                return result;
            };
        }
    }
}
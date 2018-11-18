using Nancy;
using Nancy.ModelBinding;
using ServiceInterfaces;
using Web.Models;


namespace Web.Modules
{
    public class Login : NancyModule
    {
        public Login(IAccountController accountController)
        {
            Get["/"] = parameters =>
            {
                return View["Home/Login.cshtml"];
            };
            Post["/api/login"] = parameters =>
            {
                var data = this.Bind<Credits>();
                return Response.AsJson(accountController.IsGranted(data.Login, data.Password));
            };
        }
    }
}
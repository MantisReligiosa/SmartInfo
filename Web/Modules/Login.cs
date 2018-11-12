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
            Get["/"] = parameters => View["Login.cshtml"];
            Post["/api/login"] = parameters =>
            {
                var data = this.Bind<Credits>();
                if (accountController.IsGranted(data.Login, data.Password))
                {
                    // ToDo: обратиться к базе за учеткой
                    return Response.AsJson(data.Login.Equals("1")&&data.Password.Equals("1"));
                }
                return Response.AsJson(false);
            };
        }
    }
}
using ServiceInterfaces;

namespace Services
{
    public class AccountController : IAccountController
    {
        public bool IsGranted(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return false;
            // ToDo: обратиться к базе за учеткой
            return login.Equals("1") && password.Equals("1");
        }
    }
}

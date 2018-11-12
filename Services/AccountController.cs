using ServiceInterfaces;

namespace Services
{
    public class AccountController : IAccountController
    {
        public bool IsGranted(string login, string password)
        {
            return true;
        }
    }
}

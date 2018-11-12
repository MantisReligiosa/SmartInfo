namespace ServiceInterfaces
{
    public interface IAccountController
    {
        bool IsGranted(string login, string password);
    }
}

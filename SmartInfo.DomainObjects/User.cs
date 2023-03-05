using DomainObjects;

namespace DomainObjects;

public class User : Identity
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}
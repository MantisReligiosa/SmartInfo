using System;

namespace DomainObjects
{
    public class User
    {
        public string Login { get; set; }
        public Guid Id { get; set; }
        public string PasswordHash { get; set; }
    }
}

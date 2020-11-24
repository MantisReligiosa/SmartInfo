using System;

namespace DomainObjects
{
    public class User : Identity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Guid GUID { get; set; }
    }
}

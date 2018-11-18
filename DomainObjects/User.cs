using System;

namespace DomainObjects
{
    public class User
    {
        public string Login { get; set; }
        public Guid Identifier { get; set; }

        public bool IsPasswordCorrect(string password)
        {
            return password.Equals("1");
        }
    }
}

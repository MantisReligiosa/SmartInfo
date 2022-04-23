using Nancy.Security;
using System.Collections.Generic;

namespace Web.Models
{
    public class AuthenticatedUser : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
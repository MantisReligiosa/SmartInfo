using ServiceInterfaces;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class CryptoProvider : ICryptoProvider
    {
        public string Hash(byte[] bytes)
        {
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            var hashString = new StringBuilder();
            foreach (var x in hash)
            {
                hashString.Append($"{x:x2}");
            }
            return hashString.ToString();
        }

        public string Hash(string str)
        {
            var bytes = Encoding.Unicode.GetBytes(str);
            return Hash(bytes);
        }
    }
}

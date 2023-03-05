using System.Security.Cryptography;
using System.Text;
using SmartInfo.ServiceInterfaces;

namespace SmartInfo.Services;

public class CryptoProvider : ICryptoProvider
{
    public string Hash(byte[] bytes)
    {
        var hash = SHA256.HashData(bytes);
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
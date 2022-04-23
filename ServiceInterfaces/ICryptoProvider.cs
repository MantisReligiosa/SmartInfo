namespace ServiceInterfaces
{
    public interface ICryptoProvider
    {
        string Hash(byte[] bytes);
        string Hash(string str);
    }
}

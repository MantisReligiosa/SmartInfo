namespace ServiceInterfaces
{
    public interface IConfiguration
    {
        string ConnectionString { get; }
        string BrokerType { get; }
    }
}

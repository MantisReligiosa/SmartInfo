using ServiceInterfaces;

namespace SmartInfo.Services;

public class Configuration : IConfiguration
{
    public string BrokerType => GetAppString("BrokerType");

    private static string GetAppString(string paramName)
    {
        throw new NotImplementedException();
    }
}
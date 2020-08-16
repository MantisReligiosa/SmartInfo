using ServiceInterfaces;
using System;
using System.Configuration;

namespace Services
{
    public class Configuration : IConfiguration
    {
        public string BrokerType => GetAppString("BrokerType");

        private static string GetAppString(string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
                throw new ArgumentNullException("paramName");

            return ConfigurationManager.AppSettings[paramName];
        }
    }
}

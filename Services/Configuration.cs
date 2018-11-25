using ServiceInterfaces;
using System;
using System.Configuration;

namespace Services
{
    public class Configuration : IConfiguration
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["Display_control.Properties.Settings.DefaultConnection"].ConnectionString;

        private static string GetAppString(string paramName)
        {
            if (String.IsNullOrEmpty(paramName))
                throw new ArgumentNullException("paramName");

            return ConfigurationManager.AppSettings[paramName];
        }
    }
}

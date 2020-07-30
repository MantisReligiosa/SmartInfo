using Setup.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Setup.Managers
{
    public static class ConfigurationManager
    {
        internal static string GetConnectionString(ConfigurationFilesContext context)
        {
            var configFilePath = Path.Combine(
                context.InstallDir, Constants.ConfigFileSource);
            var webConfig = XDocument.Load(configFilePath);
            return webConfig.Root.Element("Configuration").Element("connectionStrings").Elements().First().Attribute("connectionString").Value;
        }
    }
}

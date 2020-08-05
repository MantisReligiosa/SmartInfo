using Setup.Data;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Setup.Managers
{
    internal static class ConfigurationManager
    {
        public static string GetConnectionString(ConfigurationFilesContext context)
        {
            var configFilePath = Path.Combine(
                context.InstallDir, Constants.ConfigFileSource);
            var xmlDocument = XDocument.Load(configFilePath);
            var element = xmlDocument.XPathSelectElement(
                $"configuration/connectionStrings/add[@name='DefaultConnection']");
            return element.Attribute("connectionString").Value;
        }
    }
}

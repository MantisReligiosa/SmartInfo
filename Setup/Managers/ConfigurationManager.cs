using Setup.Data;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Setup.Managers
{
    public static class ConfigurationManager
    {
        internal static void CorrectConfigurationFiles(ConfigurationFilesContext context)
        {
            var configFilePath = Path.Combine(
                context.InstallDir, Constants.ConfigFileSource);
            var webConfig = XDocument.Load(configFilePath);

            SetXmlConnectionString(Properties.ConnectionString.ConfigPropertyName,
                context.ConnectionString ?? Properties.ConnectionString.DefaultValue, webConfig);

            webConfig.Save(configFilePath);
        }

        private static void SetXmlConnectionString(string connectionStringName, string connectionStringValue,
            XDocument xmlDocument)
        {
            var element = xmlDocument.XPathSelectElement(
                $"configuration/connectionStrings/add[@name='{connectionStringName}']");

            if (element != null)
                element.Attribute("connectionString").SetValue(connectionStringValue);
        }
    }
}

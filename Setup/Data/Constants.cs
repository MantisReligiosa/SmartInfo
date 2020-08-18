namespace Setup.Data
{
    internal static class Constants
    {

        public static string PublishFolder => $@"..\_publish\{ProductName}";

        public const string Manufacturer = "Smart Technologies-M";

        public const string ProductName = "SmartInfo";

        public static string ExecFile => $"{ProductName}.exe";

        public static string CommonInstallationName => $"{ProductName}";

        public static string InstallationDirectory => $@"%ProgramFiles%\{Manufacturer}\{ProductName}";

        public static string ProgramMenuDirectory => $@"%ProgramMenu%\{Manufacturer}\{ProductName}";

        public static string ConfigFileSource => $"{ProductName}.exe.config";

        internal static string[] DependentAssemblies => new string[] { };

        public static object LogPrefix => "<***> ";
    }
}

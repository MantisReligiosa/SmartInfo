namespace Setup.Data
{
    public static class Constants
    {

        public static string PublishFolder => $@"..\{ProductName}\bin";

        public const string Manufacturer = "Smart Technologies-M";

        public const string ProductName = "Display-control";

        public static string ExecFile => $"{ProductName}.exe";

        public static string CommonInstallationName => $"{ProductName}";

        public static string InstallationDirectory => $@"%ProgramFiles%\{Manufacturer}\{ProductName}";

        public static string ProgramMenuDirectory => $@"%ProgramMenu%\{Manufacturer}\{ProductName}";

        public static string ConfigFileSource => $"{ProductName}.exe.config";

        internal static string[] DependentAssemblies => new string[] { };

        public static object LogPrefix => "*** ";
    }
}

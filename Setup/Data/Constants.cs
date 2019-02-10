namespace Setup.Data
{
    public static class Constants
    {

        public static string PublishFolder => $@"..\_publish\{ProductName}";

        public const string Manufacturer = "Smart Technologies-M";

        public const string ProductName = "Display-control";

        public static string ExecFile => $"{ProductName}.exe";

        public static string CommonInstallationName => $"{Manufacturer} {ProductName}";

        public static string InstallationDirectory => $@"%ProgramFiles%\{Manufacturer}\{ProductName}";

        public static string ProgramMenuDirectory => $@"%ProgramMenu%\{Manufacturer}\{ProductName}";

        public const string ConfigFileSource = "web.config";

        internal static string[] DependentAssemblies => new string[] { };
    }
}

namespace Setup.Data
{
    internal static class Constants
    {
        public static string PublishFolder => $@"..\_publish\{ProductName}";

        public static string Manufacturer => "Smart Technologies-M";

        public static string ProductName => "SmartInfo";

        public static string ExecFile => $"{ProductName}.exe";

        public static string CommonInstallationName => $"{ProductName}";

        public static string InstallationDirectory => $@"%ProgramFiles%\{Manufacturer}\{ProductName}";

        public static string ProgramMenuDirectory => $@"%ProgramMenu%\{Manufacturer}\{ProductName}";

        public static string NewerProductInstalledErrorMessage => "Newer version already installed";

        internal static string[] DependentAssemblies => new string[] { };

        public static object LogPrefix => "<***> ";
    }
}

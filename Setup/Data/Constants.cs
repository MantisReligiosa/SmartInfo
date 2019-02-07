namespace Setup.Data
{
    public static class Constants
    {

        public const string PublishFolder = @"..\..\_publish";

        public const string Manufacturer = "Smart Technologies-M";

        public const string ProductName = "Display-control";

        public static string ExecFile => $"{ProductName}.exe";

        public static string CommonInstallationName => $"{Manufacturer} {ProductName}";

        public static string InstallationDirectory => $@"%ProgramFiles%\{Manufacturer}\{ProductName}";

        public static string ProgramMenuDirectory => $@"%ProgramMenu%\{Manufacturer}\{ProductName}";

        public const string ConfigFileSource = "web.config";

        internal static string[] DependentAssemblies => new[]
        {
            "BCrypt.Net-Next.dll"
        };
    }
}

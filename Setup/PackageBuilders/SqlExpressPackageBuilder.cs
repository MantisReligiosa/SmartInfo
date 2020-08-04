using Setup.Interfaces;
using System;
using System.Net;
using WixSharp.Bootstrapper;

namespace Setup.Packages
{
    internal class SqlExpressPackageBuilder : IPackageBuilder
    {
        private readonly string _sourceFile = @"SQL2019-SSEI-Expr.exe";

        public ChainItem Make(Guid guid, Version version)
        {
            string address = @"https://download.microsoft.com/download/7/f/8/7f8a9c43-8c8a-4f7c-9f92-83c18d96b681/SQL2019-SSEI-Expr.exe";
            using (var client = new WebClient())
            {
                client.DownloadFile(address, _sourceFile);
            }
            var package = new ExePackage("Setup.bat")
            {
                DisplayName = "Installing Microsoft® SQL Server® 2019 Express Edition",
                Compressed = true,
                InstallCommand = $"'[WixBundleLastUsedSource]{_sourceFile}' /q /CONFIGURATIONFILE=SqlExpress.ini /IAcceptSQLServerLicenseTerms",
                InstallCondition = "Not SqlInstanceFoundx64",
                DetectCondition = "SqlInstanceFoundx64",
                PerMachine = true,
                Payloads = new Payload[] { new Payload("SqlExpress.ini"), new Payload(_sourceFile) }
            };
            return package;
        }
    }
}

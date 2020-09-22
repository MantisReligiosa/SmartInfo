using Setup.Data;
using Setup.Interfaces;
using Setup.Managers;
using Setup.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using WixSharp;
using WixSharp.Bootstrapper;

namespace Setup
{
    public class Setup
    {
        private static void Main(string[] args)
        {
            var path = Path.Combine(Constants.PublishFolder, Constants.ExecFile);
            AssemblyManager.GetAssemblyInfo(path, out Guid guid, out Version version);

            IPackageFactory packageFactory = new PackageFactory(new List<IPackageBuilder>
            {
                new DotNetPackageBuilder(),
                new SqlExpressPackageBuilder(),
                new ProgramPackageBuilder(),
            });

            var bootstrapper = new Bundle($"{Constants.ProductName}", packageFactory.GetPackages(guid, version))
            {
                UpgradeCode = guid,
                Version = version,
                IconFile = @"..\SmartInfo\Resources\Logo.ico",
                Manufacturer = Constants.Manufacturer
            };
            bootstrapper.Application.LogoFile = @"..\SmartInfo\Resources\Logo.bmp";
            bootstrapper.Application.SuppressOptionsUI = false;
            bootstrapper.Application.SuppressRepair = false;
            bootstrapper.Include(WixExtension.NetFx);
            bootstrapper.AddWixFragment("Wix/Bundle",
               new UtilRegistrySearch()
               {
                   Root = RegistryHive.LocalMachine,
                   Key = @"SYSTEM\CurrentControlSet\Services\MSSQL$SQLSRV_SMRT",
                   Result = SearchResult.exists,
                   Variable = "SQLSERVERINSTALLED",
               });
            bootstrapper.Build("Installer.exe");
        }
    }
}

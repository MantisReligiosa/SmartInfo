using Microsoft.Deployment.WindowsInstaller;
using Setup.Data;
using Setup.Interfaces;
using Setup.Managers;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using WixSharp;
using WixSharp.Bootstrapper;
using WixSharp.Controls;
using static WixSharp.SetupEventArgs;
using IO = System.IO;

namespace Setup
{
    public class Setup
    {
        private static readonly ISqlManager _sqlManager = new MsSqlManager();

        private static void Main(string[] args)
        {
            var path = Path.Combine(Constants.PublishFolder, Constants.ExecFile);
            Debug.Write(path.ToString());
            AssemblyManager.GetAssemblyInfo(path, out Guid guid, out Version version);


            var productMsi = BuildMsi(guid, version);
            var sqlPackage = SqlExpress();
            var bootstrapper = new Bundle($"{Constants.ProductName}",
                                            new PackageGroupRef("NetFx462Web"),
                                            sqlPackage,
                                            new MsiPackage(productMsi)
                                            {
                                                DisplayInternalUI = true,
                                                Compressed = true
                                            })
            {
                UpgradeCode = guid,
                Version = version,
                IconFile = @"..\Display-control\Resources\Logo.ico",
                Manufacturer = Constants.Manufacturer,
            };
            bootstrapper.Application.LogoFile = @"..\Display-control\Resources\Logo.bmp";
            bootstrapper.Application.SuppressOptionsUI = true;
            bootstrapper.Include(WixExtension.NetFx);
            bootstrapper.Build($"{Constants.ProductName} Installer.exe");
            IO.File.Delete(productMsi);
            IO.File.Delete(sqlPackage.SourceFile);
        }

        private static ExePackage SqlExpress()
        {
            string address = @"https://download.microsoft.com/download/7/f/8/7f8a9c43-8c8a-4f7c-9f92-83c18d96b681/SQL2019-SSEI-Expr.exe";
            string fileName = @"SQL2019-SSEI-Expr.exe";
            using (var client = new WebClient())
            {
                client.DownloadFile(address, fileName);
            }
            var package = new ExePackage(fileName)
            {
                Compressed = true,
                InstallCommand = @"/q /Action=Install /IAcceptSQLServerLicenseTerms",
                PerMachine = true,
            };
            return package;
        }

        private static string BuildMsi(Guid guid, Version version)
        {
            const string scriptFolder = "Script";
            const string viewsFolder = "Views";
            const string assetsFolder = "assets";
            const string cssFolder = "css";
            const string imagesFolder = "Images";
            const string appFolder = "app";
            const string vendorFolder = "Vendor";
            const string codesFolder = "Codes";
            const string homeFolder = "Home";
            const string blocksFolder = "Blocks";
            const string rendersFolder = "Renders";
            const string propertiesFolder = "Properties";
            const string sharedFolder = "Shared";
            const string anyFilesMask = "*.*";
            var project = new ManagedProject("setup",
                    new Dir(Constants.InstallationDirectory,
                        new DirFiles(Path.Combine(Constants.PublishFolder, anyFilesMask)),
                        new Dir(assetsFolder, new DirFiles(Path.Combine(Constants.PublishFolder, assetsFolder, anyFilesMask))),
                        new Dir(cssFolder, new DirFiles(Path.Combine(Constants.PublishFolder, cssFolder, anyFilesMask))),
                        new Dir(imagesFolder, new DirFiles(Path.Combine(Constants.PublishFolder, imagesFolder, anyFilesMask))),
                        new Dir(scriptFolder,
                            new Dir(appFolder, new DirFiles(Path.Combine(Constants.PublishFolder, scriptFolder, appFolder, anyFilesMask))),
                            new Dir(vendorFolder, new DirFiles(Path.Combine(Constants.PublishFolder, scriptFolder, vendorFolder, anyFilesMask)))),
                        new Dir(viewsFolder, new DirFiles(Path.Combine(Constants.PublishFolder, viewsFolder, anyFilesMask)),
                            new Dir(codesFolder, new DirFiles(Path.Combine(Constants.PublishFolder, viewsFolder, codesFolder, anyFilesMask))),
                            new Dir(homeFolder, new DirFiles(Path.Combine(Constants.PublishFolder, viewsFolder, homeFolder, anyFilesMask)),
                                new Dir(blocksFolder, new DirFiles(Path.Combine(Constants.PublishFolder, viewsFolder, homeFolder, blocksFolder, anyFilesMask)),
                                    new Dir(rendersFolder, new DirFiles(Path.Combine(Constants.PublishFolder, viewsFolder, homeFolder, blocksFolder, rendersFolder, anyFilesMask)))),
                                new Dir(propertiesFolder, new DirFiles(Path.Combine(Constants.PublishFolder, viewsFolder, homeFolder, propertiesFolder, anyFilesMask)))),
                            new Dir(sharedFolder, new DirFiles(Path.Combine(Constants.PublishFolder, viewsFolder, sharedFolder, anyFilesMask))))
                        ),
                    new Dir(Constants.ProgramMenuDirectory,
                            new ExeFileShortcut($"Uninstall {Constants.ProductName}", "[System64Folder]msiexec.exe", "/x [ProductCode]"),
                            new ExeFileShortcut(Constants.ProductName, $"[INSTALLDIR]{Constants.ExecFile}", arguments: "")),
                    new Dir(@"%Desktop%",
                            new ExeFileShortcut(Constants.ExecFile, $"[INSTALLDIR]{Constants.ExecFile}", arguments: "")))
            {
                GUID = guid,
                Description = Constants.CommonInstallationName,
                InstallPrivileges = InstallPrivileges.elevated,
                MajorUpgradeStrategy = new MajorUpgradeStrategy
                {
                    UpgradeVersions = VersionRange.OlderThanThis,
                    PreventDowngradingVersions = VersionRange.ThisAndNewer,
                    NewerProductInstalledErrorMessage = Messages.NewerProductInstalledErrorMessage,
                    RemoveExistingProductAfter = Step.InstallInitialize
                },
                InstallScope = InstallScope.perMachine,
                Version = version,
                UI = WUI.WixUI_InstallDir,
                CustomUI = new DialogSequence()
                                   .On(NativeDialogs.WelcomeDlg, Buttons.Next, new ShowDialog(NativeDialogs.InstallDirDlg))
                                   .On(NativeDialogs.InstallDirDlg, Buttons.Back, new ShowDialog(NativeDialogs.WelcomeDlg)),
                BannerImage = @"..\Display-control\Resources\Logo.bmp"
            };
            project.ControlPanelInfo.Manufacturer = Constants.Manufacturer;
            project.DefaultRefAssemblies.AddRange(
                AssemblyManager.GetAssemblyPathsCollection(Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location)));
            project.AfterInstall += Project_AfterInstall;
            project.UIInitialized += Project_UIInitialized;
            return project.BuildMsi();
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleTitle(string title);

        private static void Project_UIInitialized(SetupEventArgs e)
        {
            e.Session.Log($"{Constants.LogPrefix}{MethodBase.GetCurrentMethod().Name}");
        }

        private static void Project_AfterInstall(SetupEventArgs e)
        {
            e.Session.Log($"{Constants.LogPrefix}{MethodBase.GetCurrentMethod().Name}");
            if (e.IsUpgrading)
                return;

            if (e.IsUninstalling)
                return;

            var installDir = e.Session.Property(Parameters.InstallationDirectoryParameter);
            e.Session.Log($"{Constants.LogPrefix}{nameof(installDir)}='{installDir}'");
            try
            {
                var connectionString = ConfigurationManager.GetConnectionString(
                        new ConfigurationFilesContext
                        {
                            InstallDir = installDir
                        });

                _sqlManager.LogRecieved += (sender, logEventArgs) =>
                {
                    Console.WriteLine(logEventArgs.Log);
                    e.Session.Log(logEventArgs.Log);
                };
                AllocConsole();
                SetConsoleTitle("migrate.exe");
                _sqlManager.CreateDatabase(connectionString);
                _sqlManager.ApplyMigrations(Path.Combine(e.InstallDir, "migrate.exe"), connectionString);

            }
            catch (Exception ex)
            {
                e.Session.Log(ex.ToString());
                NotificationManager.ShowErrorMessage(ex.Message,
                    isWizardInstallation: IsWizardInstallationMode(e.Data));

                e.Result = ActionResult.Failure;
                return;
            }
        }

        private static bool IsWizardInstallationMode(AppData data)
        {
            return bool.TryParse(data[Parameters.WizardInstallationParameter], out bool result) && result;
        }
    }
}

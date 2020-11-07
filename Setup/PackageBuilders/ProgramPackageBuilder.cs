using Microsoft.Deployment.WindowsInstaller;
using Setup.Data;
using Setup.Interfaces;
using Setup.Managers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using WixSharp;
using WixSharp.Bootstrapper;
using WixSharp.Controls;

namespace Setup.Packages
{
    internal class ProgramPackageBuilder : IPackageBuilder
    {
        private string _msiPath;

        public ChainItem Make(Guid guid, Version version)
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
            var project = new ManagedProject(Constants.CommonInstallationName,
                    new Dir(Constants.InstallationDirectory,
                        new DirFiles(Path.Combine(Constants.PublishFolder, anyFilesMask)),
                        new WixSharp.File("\\..\\packages\\EntityFramework.6.2.0\\tools\\migrate.exe"),
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
                            new ExeFileShortcut(Constants.ExecFile, $"[INSTALLDIR]{Constants.ExecFile}", arguments: ""))
                    )
            {
                GUID = guid,
                Description = Constants.CommonInstallationName,
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
            };
            project.BeforeInstall += Project_BeforeInstall;
            project.AfterInstall += Project_AfterInstall;
            project.ControlPanelInfo.Manufacturer = Constants.Manufacturer;
            project.DefaultRefAssemblies.AddRange(
                AssemblyManager.GetAssemblyPathsCollection(Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location)));
            _msiPath = project.BuildMsi();
            return new MsiPackage(_msiPath);
        }

        private void Project_AfterInstall(SetupEventArgs e)
        {
            if (e.IsInstalling)
            {
                CustomActions.OnInstall(e.Session);
            }
        }

        private void Project_BeforeInstall(SetupEventArgs e)
        {
            if (e.IsInstalling)
            {
                Process[] pname = Process.GetProcesses();
                if (pname.Any(p=>p.ProcessName.Contains(Constants.ProductName)))
                {
                    NotificationManager.ShowErrorMessage($"{Constants.ProductName} сейчас запущен.\r\nПеред установкой необходимо завершить работу текущей версии программы");
                    e.Result = ActionResult.Failure;
                }
            }
        }
    }

    public class CustomActions
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleTitle(string title);

        [CustomAction]
        public static ActionResult OnInstall(Session session)
        {
            return DeployDatabase(session) ? ActionResult.Success : ActionResult.Failure;
        }

        private static bool DeployDatabase(Session session)
        {
            ISqlManager _sqlManager = new MsSqlManager();
            _sqlManager.LogRecieved += (sender, logEventArgs) =>
            {
                session.Log(logEventArgs.Log);
            };
            var installDir = session.Property(Parameters.InstallationDirectoryParameter);
            session.Log($"{Constants.LogPrefix}{nameof(installDir)}='{installDir}'");
            try
            {
                var connectionString = ConfigurationManager.GetConnectionString(
                        new ConfigurationFilesContext
                        {
                            InstallDir = installDir
                        });
                session.Log($"{Constants.LogPrefix}{nameof(connectionString)}='{connectionString}'");
                AllocConsole();
                SetConsoleTitle("migrate.exe");
                _sqlManager.ApplyMigrations(Path.Combine(installDir, "migrate.exe"), connectionString);
            }
            catch (Exception ex)
            {
                session.Log(ex.ToString());
                return false;
            }
            return true;
        }
    }
}

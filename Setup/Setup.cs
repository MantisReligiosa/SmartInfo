using Microsoft.Deployment.WindowsInstaller;
using Setup.CustomDialogs;
using Setup.Data;
using Setup.Interfaces;
using Setup.Managers;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using WixSharp;
using WixSharp.Forms;
using static WixSharp.SetupEventArgs;

namespace Setup
{
    public class Setup
    {
        private static readonly ISqlManager _sqlManager = new MsSqlManager();

        private static void Main(string[] args)
        {
            var path = System.IO.Path.Combine(Constants.PublishFolder, Constants.ExecFile);
            Debug.Write(path.ToString());
            AssemblyManager.GetAssemblyInfo(path, out Guid guid, out Version version);

            var managedUI = new ManagedUI();
            managedUI.InstallDialogs
                .Add(Dialogs.Welcome)
                .Add(Dialogs.InstallDir)
                .Add<ConnectionStringDialog>()
                .Add(Dialogs.Progress)
                .Add(Dialogs.Exit);

            managedUI.ModifyDialogs
                .Add(Dialogs.MaintenanceType)
                .Add(Dialogs.Progress)
                .Add(Dialogs.Exit);

            var project = new ManagedProject(Constants.CommonInstallationName,
                new Dir(Constants.InstallationDirectory,
                    new DirFiles(Path.Combine(Constants.PublishFolder, "*.*")),
                    new Dir("css", new DirFiles(Path.Combine(Constants.PublishFolder, "css", "*.*"))),
                    new Dir("Images", new DirFiles(Path.Combine(Constants.PublishFolder, "Images", "*.*"))),
                    new Dir("Script",
                        new Dir("app", new DirFiles(Path.Combine(Constants.PublishFolder, "Script", "app", "*.*"))),
                        new Dir("Vendor", new DirFiles(Path.Combine(Constants.PublishFolder, "Script", "Vendor", "*.*")))),
                    new Dir("Views", new DirFiles(Path.Combine(Constants.PublishFolder, "Views", "*.*")),
                        new Dir("Codes", new DirFiles(Path.Combine(Constants.PublishFolder, "Views", "Codes", "*.*"))),
                        new Dir("Home", new DirFiles(Path.Combine(Constants.PublishFolder, "Views", "Home", "*.*"))),
                        new Dir("Shared", new DirFiles(Path.Combine(Constants.PublishFolder, "Views", "Shared", "*.*"))))
                    ),
                new Dir(Constants.ProgramMenuDirectory,
                        new ExeFileShortcut($"Uninstall {Constants.ProductName}", "[System64Folder]msiexec.exe", "/x [ProductCode]"),
                        new ExeFileShortcut(Constants.ProductName, "[INSTALLDIR]Display-control.exe", arguments: "")),
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
                ManagedUI = managedUI,
                Version = version,
            };
            project.ControlPanelInfo.Manufacturer = Constants.Manufacturer;
            project.DefaultRefAssemblies.AddRange(
                AssemblyManager.GetAssemblyPathsCollection(System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location)));
            project.AfterInstall += Project_AfterInstall;
            project.UIInitialized += Project_UIInitialized;
            Compiler.BuildMsi(project);
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleTitle(string title);

        private static void Project_UIInitialized(SetupEventArgs e)
        {
            e.Session.Log($"{Constants.LogPrefix}{MethodBase.GetCurrentMethod().Name}");

            e.Data[Properties.ConnectionString.PropertyName] = Properties.ConnectionString.DefaultValue;
            e.Session[Properties.ConnectionString.PropertyName] = Properties.ConnectionString.DefaultValue;
        }

        private static void Project_AfterInstall(SetupEventArgs e)
        {
            e.Session.Log($"{Constants.LogPrefix}{MethodBase.GetCurrentMethod().Name}");
            if (e.IsUpgrading)
                return;

            if (e.IsUninstalling)
                return;

            var installDir = e.Session.Property(Parameters.InstallationDirectoryParameter);
            var connectionString = e.Data[Properties.ConnectionString.PropertyName];
            e.Session.Log($"{Constants.LogPrefix}{nameof(installDir)}='{installDir}'");
            e.Session.Log($"{Constants.LogPrefix}{nameof(connectionString)}='{connectionString}'");
            try
            {
                ConfigurationManager.CorrectConfigurationFiles(
                        new ConfigurationFilesContext
                        {
                            InstallDir = installDir,
                            ConnectionString = connectionString
                        });

                _sqlManager.LogRecieved += (sender, logEventArgs) =>
                {
                    Console.WriteLine(logEventArgs.Log);
                    e.Session.Log(logEventArgs.Log);
                };
                AllocConsole();
                SetConsoleTitle("migrate.exe");
                _sqlManager.CreateDatabase(connectionString);
                var processToStart = Path.Combine(e.InstallDir, "migrate.exe");
                _sqlManager.ApplyMigrations(processToStart);

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
            return bool.TryParse(data[Parameters.WizardInstallationParameter], out bool result)
                ? result
                : false;
        }
    }
}

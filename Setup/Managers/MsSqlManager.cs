using Setup.Data;
using Setup.Interfaces;
using System;

namespace Setup.Managers
{
    internal class MsSqlManager : ISqlManager
    {
        public event EventHandler<LogEventArgs> LogRecieved;

        public void ApplyMigrations(string processToStart, string connectionString)
        {

            var parameters = $"Repository.dll /connectionString=\"{connectionString}\" /connectionProviderName=\"System.Data.SqlClient\" /verbose";

            var processManager = new ProcessManager();

            processManager.OutputDataRecieved += (sender, processDataEventsArgs) =>
                 LogRecieved?.Invoke(this, new LogEventArgs(processDataEventsArgs.Data));

            var exitCode = processManager.ExecProcess(processToStart, parameters);
            if (exitCode != 0)
                throw new Exceptions.SqlException($"Ошибка применения миграций к БД. (Код ошибки {exitCode})");
        }
    }
}

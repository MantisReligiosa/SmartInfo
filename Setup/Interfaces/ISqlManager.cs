using Setup.Data;
using System;

namespace Setup.Interfaces
{
    public interface ISqlManager
    {
        event EventHandler<LogEventArgs> LogRecieved;

        void ApplyMigrations(string installDir);

        void ValidateConnectionString(string connectionString);

        void CreateDatabase(string connectionString);

        void CheckServerAvailability(string connectionString);

        bool IsUserHasRestoreDatabaseRights(string connectionString);

        bool IsDatabaseExists(string connectionString);
    }
}

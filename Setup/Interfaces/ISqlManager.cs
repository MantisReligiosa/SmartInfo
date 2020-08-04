using Setup.Data;
using System;

namespace Setup.Interfaces
{
    internal interface ISqlManager
    {
        event EventHandler<LogEventArgs> LogRecieved;

        void ApplyMigrations(string installDir, string connectionString);
    }
}

namespace Setup.Interfaces
{
    public interface ISqlManager
    {
        void ApplyMigrations(string installDir);

        void ValidateConnectionString(string connectionString);

        void CreateDatabase(string connectionString);

        void CheckServerAvailability(string connectionString);

        bool IsUserHasRestoreDatabaseRights(string connectionString);

        bool IsDatabaseExists(string connectionString);
    }
}

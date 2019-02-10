using Setup.Data;
using Setup.Interfaces;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Setup.Managers
{
    public class MsSqlManager : ISqlManager
    {
        public void ValidateConnectionString(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
                try
                {
                    connection.Open();
                }
                catch (System.Exception ex)
                {
                    throw new Exceptions.SqlException(
                        "Не удалось установить соединение с БД с помощью строки подключения",
                        ex);
                }
        }

        public void CheckServerAvailability(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            ValidateConnectionString(builder.ConnectionString);
        }

        public bool IsUserHasRestoreDatabaseRights(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            var query = $@"SELECT permission_name
                           FROM fn_my_permissions(NULL, 'DATABASE')
                           WHERE permission_name = 'CREATE DATABASE'";

            using (var connection = new SqlConnection(builder.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                    return reader.HasRows;
            }
        }

        public void CreateDatabase(string connectionString)
        {
            if (IsDatabaseExists(connectionString))
                return;

            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;

            builder.InitialCatalog = "master";

            var query =
                 $@"CREATE DATABASE {databaseName}";

            using (var connection = new SqlConnection(builder.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandTimeout = 60;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Метод для проверки существования базы данных
        /// </summary>
        /// <param name="connectionString">Строка подключения</param>
        /// <returns>Возвращает признак, указывающий существует ли база данных</returns>
        public bool IsDatabaseExists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            var query = $@"SELECT [name]
                           FROM sys.databases
                           WHERE [name] = '{builder.InitialCatalog}'";

            builder.InitialCatalog = "master";

            using (var connection = new SqlConnection(builder.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                    return reader.HasRows;
            }
        }

        public void ApplyMigrations(string processToStart)
        {
            
            var parameters = $"Repository.dll /startupConfigurationFile=\"web.dll.config\"";

            using (var process = Process.Start(processToStart, parameters))
                process.WaitForExit();
        }
    }
}

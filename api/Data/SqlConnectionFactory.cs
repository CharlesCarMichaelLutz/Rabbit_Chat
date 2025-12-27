using Npgsql;
using System.Data;

namespace api.Data
{
    public interface ISqlConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new NpgsqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();

            }
            catch
            {
                throw new NpgsqlException();
            }
            return connection;
        }
    }
}

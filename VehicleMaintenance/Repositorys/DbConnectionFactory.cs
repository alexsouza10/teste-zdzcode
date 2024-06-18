using Microsoft.Data.SqlClient;
using System.Data;

namespace VehicleMaintenance.Repositorys
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IDbConnection> OpenConnectionAsync()
        {
            try
            {
                var connection = CreateConnection();
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir a conexão com o banco de dados: {ex.Message}");
                throw; 
            }
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            try
            {
                var connection = CreateConnection();
                connection.Open();
                return connection.BeginTransaction(isolationLevel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar a transação: {ex.Message}");
                throw;
            }
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

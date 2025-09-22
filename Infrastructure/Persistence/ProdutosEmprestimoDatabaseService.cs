using Microsoft.Data.Sqlite;
using System.Data;

namespace Infrastructure.Persistence
{
    public class ProdutosEmprestimoDatabaseService
    {
        private readonly SqliteConnection _masterConnection;
        private readonly string _connectionString;

        public ProdutosEmprestimoDatabaseService(string connectionString)
        {
            _connectionString = connectionString;
            _masterConnection = new SqliteConnection(connectionString);
            _masterConnection.Open();
            InitializeDatabase();
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void InitializeDatabase()
        {
            using var cmd = _masterConnection.CreateCommand();
            cmd.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS ProdutosEmprestimos 
                (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    TaxaJurosAnual REAL NOT NULL,
                    PrazoMaximoMeses INTEGER NOT NULL
                )
            ";
            cmd.ExecuteNonQuery();
        }
    }
}

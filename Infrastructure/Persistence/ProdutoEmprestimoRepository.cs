using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Persistence
{
    public class ProdutoEmprestimoRepository : IProdutoEmprestimoRepository
    {
        private readonly ProdutosEmprestimoDatabaseService _databaseService;

        public ProdutoEmprestimoRepository(ProdutosEmprestimoDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<ProdutoEmprestimo> GetAll()
        {
            using var conn = _databaseService.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ProdutosEmprestimos";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new ProdutoEmprestimo(reader.GetInt64(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt16(3));
            }
        }

        public ProdutoEmprestimo? GetById(long id)
        {
            using var conn = _databaseService.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText =
            @"
                SELECT *
                FROM ProdutosEmprestimos
                WHERE Id = $id
            ";
            cmd.Parameters.Add(new SqliteParameter("$id", id));

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ProdutoEmprestimo(reader.GetInt64(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt16(3));
            }
            return null;
        }

        public long Add(string nome, decimal txJurosAnual, short prazoMaxMeses)
        {
            long id;
            using var conn = _databaseService.CreateConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    cmd.CommandText =
                    @"
                        INSERT INTO ProdutosEmprestimos (Nome, TaxaJurosAnual, PrazoMaximoMeses)
                        VALUES ($nome, $taxaJurosAnual, $prazoMaximoMeses)
                    ";

                    cmd.Parameters.Add(new SqliteParameter("$nome", nome));
                    cmd.Parameters.Add(new SqliteParameter("$taxaJurosAnual", txJurosAnual));
                    cmd.Parameters.Add(new SqliteParameter("$prazoMaximoMeses", prazoMaxMeses));

                    id = Convert.ToInt64(cmd.ExecuteScalar());
                }

                transaction.Commit();
                return id;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ProdutoEmprestimo produtoEmprestimo)
        {
            using var conn = _databaseService.CreateConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    cmd.CommandText =
                    @"
                        UPDATE ProdutosEmprestimos
                        SET Nome = $nome,
                            TaxaJurosAnual = $taxaJurosAnual,
                            PrazoMaximoMeses = $prazoMaximoMeses
                        WHERE Id = $id
                    ";

                    cmd.Parameters.Add(new SqliteParameter("$id", produtoEmprestimo.Id));
                    cmd.Parameters.Add(new SqliteParameter("$nome", produtoEmprestimo.Nome));
                    cmd.Parameters.Add(new SqliteParameter("$taxaJurosAnual", produtoEmprestimo.TaxaJurosAnual));
                    cmd.Parameters.Add(new SqliteParameter("$prazoMaximoMeses", produtoEmprestimo.PrazoMaximoMeses));

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(long id)
        {
            using var conn = _databaseService.CreateConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    cmd.CommandText =
                    @"
                        DELETE FROM ProdutosEmprestimos
                        WHERE Id = $id
                    ";
                    
                    cmd.Parameters.Add(new SqliteParameter("$id", id));

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

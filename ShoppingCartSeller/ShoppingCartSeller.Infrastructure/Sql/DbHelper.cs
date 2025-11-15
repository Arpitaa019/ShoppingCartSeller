using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartSeller.Infrastructure.Sql
{
    public class DbHelper : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public DbHelper(IConfiguration configuration)
        {
              _connectionString = configuration.GetConnectionString("DefaultConnection");

              if (string.IsNullOrWhiteSpace(_connectionString))
                throw new Exception("Connection string is missing or empty. Check appsettings.json and DI setup.");

            _connection = new SqlConnection(_connectionString);
        }

        private SqlCommand CreateCommand(string query, SqlParameter[] parameters = null)
        {
            var cmd = new SqlCommand(query, _connection);
            if (_transaction != null)
                cmd.Transaction = _transaction;

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        public void BeginTransaction()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                _transaction = _connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("Error starting transaction", ex);
            }
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction.Commit();
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error committing transaction", ex);
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
            }
            catch (InvalidOperationException)
            {
                // Transaction already finished
            }
            catch (Exception ex)
            {
                throw new Exception("Error rolling back transaction", ex);
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                using var cmd = CreateCommand(query, parameters);
                cmd.CommandType = commandType;

                return await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                var sqlError = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Error executing non-query: {sqlError}", ex);
            }

        }

        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                using var cmd = CreateCommand(query, parameters);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing scalar query", ex);
            }
        }

        public async Task<DataTable> ExecuteReader(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                using var cmd = CreateCommand(query, parameters);
                cmd.CommandType = commandType;

                using var reader = await cmd.ExecuteReaderAsync();
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        public void Close()
        {
            try
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error closing connection", ex);
            }
        }

        public void Dispose()
        {
            try
            {
                _transaction?.Dispose();
                _connection?.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error disposing resources", ex);
            }
        }
    }
}
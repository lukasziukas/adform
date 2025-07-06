using Core.Common;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;

namespace Infrastructure.Repositories
{
    internal abstract class RepositoryBase
    {
        private readonly string _connectionString;

        private readonly ILogger _logger;

        protected internal IDbConnection DbConnection { get => new NpgsqlConnection(_connectionString); }

        protected RepositoryBase(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        protected async Task<DBResult<T>> ExecuteQueryFirst<T>(string sql, object parameters)
        {
            try
            {
                using var connection = DbConnection;

                var result = await connection.QueryFirstAsync<T>(sql, parameters);

                return result;
            }
            catch (NpgsqlException nex)
            {
                //TODO: process error codes
                _logger.LogError(nex, "Npgsql error occurred white executing sql statement");

                return DBError.UnknownError;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred white executing sql statement");

                return DBError.UnknownError;
            }
        }

        protected async Task<DBResult<IEnumerable<T>>> ExecuteQuery<T>(string sql, object parameters)
        {
            try
            {
                using var connection = DbConnection;

                var result = await connection.QueryAsync<T>(sql, parameters);

                return result.ToList();
            }
            catch (NpgsqlException nex)
            {
                //TODO: process error codes
                _logger.LogError(nex, "Npgsql error occurred white executing sql statement");

                return DBError.UnknownError;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred white executing sql statement");

                return DBError.UnknownError;
            }
        }
    }
}

using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AuthAccess.AuthSqlAccess
{
    public class AuthSqlAccess : IAuthSqlAccess
    {
        private readonly IConfiguration _config;


        public AuthSqlAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadUserAsync<T, U>(string storedprocedures, U parameters)
        {
            var connectionstring = _config.GetConnectionString("Default");
            using (IDbConnection connection = new SqlConnection(connectionstring))
            {
                return await connection.QueryAsync<T>(storedprocedures, parameters, commandType: CommandType.StoredProcedure);
            };
        }

        public async Task<int> ExecuteAsync<U>(string storedProcedure, U parameters)
        {
            var connectionString = _config.GetConnectionString("Default");
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            };


        }

        public async Task<T> ExecuteGenericAsync<T, U>(string storedProcedure, U parameters)
        {
            var connectionString = _config.GetConnectionString("Default");
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QuerySingleAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}


namespace AuthAccess.AuthSqlAccess
{
    public interface IAuthSqlAccess
    {
        Task<int> ExecuteAsync<U>(string storedProcedure, U parameters);
        Task<T> ExecuteGenericAsync<T, U>(string storedProcedure, U parameters);
        Task<IEnumerable<T>> LoadUserAsync<T, U>(string storedprocedures, U parameters);
    }
}
using AuthAccess.AuthSqlAccess;
using AuthAccess.Models;
using Dapper;
using System.Data;
using CsvLogger;

namespace AuthAccess.AuthService
{
    public class AuthService : IAuthService
    {
        IAuthSqlAccess _authSqlAccess;

        public AuthService(IAuthSqlAccess authSqlAccess)
        {
            _authSqlAccess = authSqlAccess;
        }


        public async Task<SessionModel> LoginAsync(LoginRequestModel requestModel)
        {

            var result = await _authSqlAccess.LoadUserAsync<LoginReturnModel, dynamic>(storedprocedures: "dbo.spRequestLoginUserInfor", new
            {
                Username = requestModel.UserName
            });
   

            var user = result.FirstOrDefault();
            CsvLogger.CsvLogger.LogInformation($" Username : {user.UserID} and Passhash: {user.PasswordHash} and Salt : {user.Salt}");

            if (user == null || !HashingMethod.VerifyPassword(requestModel.Password, user.PasswordHash, user.Salt))
            {
                CsvLogger.CsvLogger.LogInformation("can't hash");
                return null;
               
            }

         
            var session = await _authSqlAccess.ExecuteGenericAsync<SessionModel, dynamic>(storedProcedure: "dbo.spCreateUserSession", new
            {
                UserId = user.UserID
            });
            return session;
        }


        public async Task<int> RegisterUserAsync(RegisterRequestModel requestModel)
        {

            (string hash, string salt) = HashingMethod.HashPassword(requestModel.Password);

            var parameters = new DynamicParameters();
            parameters.Add("@UserName", requestModel.UserName);
            parameters.Add("@PasswordHash", hash);
            parameters.Add("@Salt", salt);
            parameters.Add("@Email", requestModel.Email);
            parameters.Add("@IsActive", true);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _authSqlAccess.ExecuteAsync("dbo.spRegisterUser", parameters);


            int result = parameters.Get<int>("@Result");
            return result;
        }


    }
}

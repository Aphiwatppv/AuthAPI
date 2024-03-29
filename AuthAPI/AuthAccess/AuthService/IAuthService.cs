using AuthAccess.Models;

namespace AuthAccess.AuthService
{
    public interface IAuthService
    {
        Task<SessionModel> LoginAsync(LoginRequestModel requestModel);
        Task<int> RegisterUserAsync(RegisterRequestModel requestModel);
    }
}
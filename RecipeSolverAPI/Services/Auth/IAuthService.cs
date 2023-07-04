using RecipeSolverAPI.Models.Auth;
using RecipeSolverAPI.Models.User;

namespace RecipeSolverAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterRequest request);
        Task<string> Verify(string verificationToken);
        Task<UserDto> Login(UserLoginRequest request);
        Task<string> ForgotPassword(ForgotPasswordRequest request);
        Task<string> ResetPassword(ResetPasswordRequest request);
        Task<UserDto> RefreshToken(TokenRequest request);
        Task<string> ChangePassword(ChangePasswordRequest request);
        string CreateRandomToken();





    }
}
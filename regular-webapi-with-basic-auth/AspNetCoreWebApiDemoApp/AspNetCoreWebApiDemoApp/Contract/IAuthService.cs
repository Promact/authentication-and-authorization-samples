using AspNetCoreWebApiDemoApp.Implementations;

namespace AspNetCoreWebApiDemoApp.Contract
{
    public interface IAuthService<TUser> where TUser : class
    {
        Task<AuthResult> RegisterUserAsync(TUser user, string password, ValidateUserOptions validateUserOptions);
        Task<AuthResult> AddPhoneNumber(TUser user, string phoneNumber, ValidateUserOptions validateUserOptions);
        Task<AuthResult> ConfirmEmilAsync(string username, string token);
        Task<AuthResult> LoginUserAsync(string email, string password);
        Task<AuthResult> ForgotPasswordAsync(string username);
        Task<AuthResult> ResetPasswordAsync(string username, string token, string password);
        Task<AuthResult> ChangePasswordAsync(string username, string oldPassword, string newPassword);
    }
}

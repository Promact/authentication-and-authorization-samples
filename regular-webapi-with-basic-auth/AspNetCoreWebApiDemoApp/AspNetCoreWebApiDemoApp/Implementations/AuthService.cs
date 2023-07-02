using AspNetCoreWebApiDemoApp.Contract;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreWebApiDemoApp.Implementations
{
    public class IdentityAuthService<TUser> : IAuthService<TUser> where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly IEmailService _emailService;
        public IdentityAuthService(SignInManager<TUser> signInManager, UserManager<TUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }
        public Task<AuthResult> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> ConfirmEmilAsync(string username, string token)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> ForgotPasswordAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> LoginUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult> RegisterUserAsync(TUser user, string password, ValidateUserOptions validateUserOptions)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (validateUserOptions.RequiresEmailConfirmation)
                {
                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // Send confirmation email to user
                    await Console.Out.WriteLineAsync(confirmationToken);
                }                
            }
        }

        public Task<AuthResult> ResetPasswordAsync(string username, string token, string password)
        {
            throw new NotImplementedException();
        }
    }
}

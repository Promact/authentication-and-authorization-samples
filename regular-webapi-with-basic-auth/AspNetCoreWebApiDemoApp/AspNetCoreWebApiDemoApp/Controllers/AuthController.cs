using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetCoreWebApiDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync(string username,string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            // Signin User using SignInManager and return JWT Token
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);            
            if (result == null || !result.Succeeded)
            {
                return BadRequest();
            }
            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync(string username, string password)
        {
            // Register User using UserManager and return JWT Token
            var user = new IdentityUser
            {
                UserName = username,
                Email = username
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Send confirmation email to user
                await Console.Out.WriteLineAsync(confirmationToken);                
            }
            if (!result.Succeeded)
            {
                return BadRequest();
            }            
            return Ok();
        }

        [HttpPost("confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmAsync(string username, string token)
        {
            // Confirm User using UserManager and return JWT Token
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("forgotpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPasswordAsync(string username)
        {
            // Forgot Password using UserManager and return JWT Token
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Send password reset email to user
            await Console.Out.WriteLineAsync(token);
            return Ok();
        }

        [HttpPost("resetpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPasswordAsync(string username, string token, string password)
        {
            // Reset Password using UserManager and return JWT Token
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            // Change Password using UserManager and return JWT Token
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("changepasswordwithtoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePasswordWithTokenAsync(string username, string token, string newPassword)
        {
            // Change Password using UserManager and return JWT Token
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("changepasswordwitholdpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePasswordWithOldPasswordAsync(string username, string oldPassword, string newPassword)
        {
            // Change Password using UserManager and return JWT Token
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("enable-two-factor-authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> EnableTwoFactorAuthenticationAsync()
        {
            // Enable Two Factor Authentication using UserManager and return JWT Token
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your_Secret_Key_Here"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(1));

            var token = new JwtSecurityToken(
                issuer: "Your_Issuer_Here",
                audience: "Your_Audience_Here",
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

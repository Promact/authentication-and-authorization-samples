using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Auth0.Demo.CustomImpl
{
    public class Auth0AuthProvider : IExternalLoginProvider, 
        IExternalLoginProviderWithPassword, 
        IExternalUserLookupServiceProvider
    {
        private readonly IManagementApiClient _managementApiClient;
        private readonly IAuthenticationApiClient _authenticationApiClient;
        public Auth0AuthProvider(IManagementApiClient managementApiClient, IAuthenticationApiClient authenticationApiClient)
        {
            _managementApiClient = managementApiClient;
            _authenticationApiClient = authenticationApiClient;
        }

        public bool CanObtainUserInfoWithoutPassword => false;

        // Create IdentityUser with userName and then Signup using Auth0 AuthenticationApiClient 
        public async Task<IdentityUser> CreateUserAsync(string userName, string providerName)
        {
            var user = new IdentityUser(Guid.NewGuid(), userName, userName);
            await _authenticationApiClient.SignupUserAsync(new SignupUserRequest
            {
                Username = userName,
                Connection = providerName,
                Email = userName,
            });
            return user;
        }

        public async Task<IdentityUser> CreateUserAsync(string userName, string providerName, string plainPassword)
        {
            var user = new IdentityUser(Guid.NewGuid(), userName, userName);
            await _authenticationApiClient.SignupUserAsync(new SignupUserRequest
            {
                Username = userName,
                Connection = providerName,
                Email = userName,
                Password = plainPassword
            });
            return user;
        }

        public async Task<IUserData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _managementApiClient.Users.GetAsync(id.ToString());
            return new UserData() {
                Id = Guid.Parse(user.UserId),
                UserName = user.Email,
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailVerified.HasValue ? user.EmailVerified.Value : false,
            };
        }

        public Task<IUserData> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEnabledAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<IUserData>> SearchAsync(string sorting = null, string filter = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryAuthenticateAsync(string userName, string plainPassword)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUserAsync(IdentityUser user, string providerName)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUserAsync(IdentityUser user, string providerName, string plainPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}

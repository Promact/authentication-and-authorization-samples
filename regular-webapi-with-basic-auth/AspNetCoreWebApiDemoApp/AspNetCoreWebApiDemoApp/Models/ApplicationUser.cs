using Microsoft.AspNetCore.Identity;

namespace AspNetCoreWebApiDemoApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

    }
}

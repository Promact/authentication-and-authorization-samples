namespace AspNetCoreWebApiDemoApp.Implementations
{
    public class ValidateUserOptions
    {
        public bool RequiresEmailConfirmation { get; set; }
        public bool RequiresUniqueEmail { get; set; }        
        public bool RequiresPhoneNumberConfirmation { get; set; }
        public bool RequiresUniquePhoneNumber { get; set; }
    }
}

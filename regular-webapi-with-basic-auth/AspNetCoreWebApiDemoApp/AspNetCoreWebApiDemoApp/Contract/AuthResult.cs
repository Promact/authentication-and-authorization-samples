namespace AspNetCoreWebApiDemoApp.Contract
{
    public class AuthResult
    {
        public int StatusCode { get; set; }
        public string StatusText { get; set; }
        public bool IsSucceded { get; set; }
        public string Token { get; set; }
    }
}
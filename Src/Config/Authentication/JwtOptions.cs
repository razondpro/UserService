namespace UserService.Config.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = String.Empty;

        public string Audience { get; set; } = String.Empty;

        public string SecretKey { get; set; } = String.Empty;

    }
}
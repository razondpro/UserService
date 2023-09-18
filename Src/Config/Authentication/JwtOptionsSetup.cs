using Microsoft.Extensions.Options;

namespace UserService.Config.Authentication
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private const string ConfigurationSectionName = "JwtOptions";
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }

}
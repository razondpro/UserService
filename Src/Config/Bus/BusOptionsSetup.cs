using Microsoft.Extensions.Options;
using Serilog;

namespace UserService.Config.Bus
{
    public class BusOptionsSetup : IConfigureOptions<BusOptions>
    {
        private const string ConfigurationSectionName = "BusOptions";

        private readonly IConfiguration _configuration;

        public BusOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(BusOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
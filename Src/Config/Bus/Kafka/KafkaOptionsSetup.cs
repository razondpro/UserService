using Microsoft.Extensions.Options;
using UserService.Config.Bus.Kafka;

namespace UserService.Config.Bus
{
    public class BusOptionsSetup : IConfigureOptions<KafkaOptions>
    {
        public const string ConfigurationSectionName = "KafkaOptions";

        private readonly IConfiguration _configuration;

        public BusOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(KafkaOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
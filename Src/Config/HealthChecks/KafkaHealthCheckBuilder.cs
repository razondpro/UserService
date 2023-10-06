using UserService.Shared.Infrastructure.Bus.Kafka.Health;

namespace UserService.Config.HealthChecks
{
    public static class KafkaHealthCheckBuilder
    {
        public static IHealthChecksBuilder AddKafkaHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddCheck<KafkaHealthCheck>("Kafka");
            return builder;
        }

    }
}
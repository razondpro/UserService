using KafkaFlow.Consumers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UserService.Shared.Infrastructure.Bus.Kafka.Health
{
    public class KafkaHealthCheck : IHealthCheck
    {
        private readonly IConsumerAccessor _consumerAccessor;

        public KafkaHealthCheck(IConsumerAccessor consumerAccessor)
        {
            _consumerAccessor = consumerAccessor;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var consumer = _consumerAccessor.All.First();
            Console.WriteLine(consumer.Status);
            if (consumer.Status is ConsumerStatus.Running)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}

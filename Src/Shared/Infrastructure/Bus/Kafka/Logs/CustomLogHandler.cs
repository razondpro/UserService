using KafkaFlow;
using Serilog;

namespace UserService.Shared.Infrastructure.Bus.Kafka.Logs
{
    public class CustomLogHandler : ILogHandler
    {
        public void Error(string message, Exception ex, object data)
        {
            Log.Error(ex, message, data);
        }

        public void Info(string message, object data)
        {
            Log.Information(message, data);
        }

        public void Verbose(string message, object data)
        {
            Log.Verbose(message, data);
        }

        public void Warning(string message, object data)
        {
            Log.Warning(message, data);
        }
    }
}
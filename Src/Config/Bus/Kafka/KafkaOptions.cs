namespace UserService.Config.Bus.Kafka
{
    public class KafkaOptions
    {
        public string[] BootstrapServers { get; set; } = Array.Empty<string>();
        public string GroupId { get; set; } = String.Empty;
        public string[] Topics { get; set; } = Array.Empty<string>();
        public string AutoOffsetReset { get; set; } = String.Empty;
        public bool EnableAutoCommit { get; set; } = false;
        public string SchemaRegistryUrl { get; set; } = String.Empty;

    }
}
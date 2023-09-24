namespace UserService.Config.Bus
{
    public class BusOptions
    {
        public string BootstrapServers { get; set; } = String.Empty;
        public string GroupId { get; set; } = String.Empty;
        public string[] Topics { get; set; } = Array.Empty<string>();
        public string AutoOffsetReset { get; set; } = String.Empty;
        public bool EnableAutoCommit { get; set; } = false;
    }
}
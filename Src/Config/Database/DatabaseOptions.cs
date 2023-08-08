namespace UserService.Config.Database
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = String.Empty;
        public int MaxRetryCount { get; set; }
        public int CommandTimeout { get; set; }
        public bool EnableDetailedErrors { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }

    }
}
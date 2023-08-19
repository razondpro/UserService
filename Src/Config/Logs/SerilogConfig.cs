namespace UserService.Config.Logs
{
    using Serilog;
    public static class SerilogConfig
    {
        public static ILogger Configure()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            const string appJsonFIle = "appsettings.json";
            string appJsonFileEnv = $"appsettings.{env}.json";

            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(appJsonFIle, false, true)
                 .AddJsonFile(appJsonFileEnv, true)
                 .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Filter.ByExcluding(e => e.Exception is FluentValidation.ValidationException)
                .CreateLogger();

            return Log.Logger;
        }

    }
}
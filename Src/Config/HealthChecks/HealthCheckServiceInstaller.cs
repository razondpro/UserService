namespace UserService.Config.HealthChecks
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using UserService.Config;
    using UserService.Config.Database;

    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql((provider) =>
                {
                    var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                    return databaseOptions.ConnectionString;
                })
                .AddKafkaHealthCheck();
            //TODO Add more health checks here
        }
    }
}
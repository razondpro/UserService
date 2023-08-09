namespace UserService.Config.Database
{
    using Shared.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();
            services.AddDbContext<Database>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                Console.WriteLine(databaseOptions);
                options.UseNpgsql(databaseOptions.ConnectionString, psgOptions =>
                {
                    psgOptions.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    psgOptions.CommandTimeout(databaseOptions.CommandTimeout);
                });
                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

            });

            return services;
        }
    }
}
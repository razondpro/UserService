namespace UserService.Config.Database
{
    using Shared.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using UserService.Shared.Infrastructure.Persistence.Core.Interceptors;

    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();
            services.AddDbContext<Database>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseNpgsql(databaseOptions.ConnectionString, psgOptions =>
                {
                    psgOptions.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    psgOptions.CommandTimeout(databaseOptions.CommandTimeout);
                });
                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

                var updateAuditableEntitiesInterceptor = provider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
                options.AddInterceptors(updateAuditableEntitiesInterceptor);

                var updateVersionedEntitiesInterceptor = provider.GetRequiredService<UpdateVersionedEntitiesInterceptor>();
                options.AddInterceptors(updateVersionedEntitiesInterceptor);

            });

            return services;
        }
    }
}
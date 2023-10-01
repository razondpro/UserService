namespace UserService.Config
{
    using System.Reflection;
    public static class DependencyInjection
    {
        public static IServiceCollection InstallServices(
            this IServiceCollection services,
            IConfiguration configuration,
            params Assembly[] assemblies)
        {
            var installers = assemblies
                .SelectMany(assembly => assembly.ExportedTypes)
                .Where(type => typeof(IServiceInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>()
                .ToList();

            installers.ForEach(installer => installer.Install(services, configuration));

            return services;
        }

        public static IHostBuilder InstallHosts(this IHostBuilder hostBuilder, IConfiguration configuration, params Assembly[] assemblies)
        {
            var installers = assemblies
                .SelectMany(assembly => assembly.ExportedTypes)
                .Where(type => typeof(IHostInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IHostInstaller>()
                .ToList();

            installers.ForEach(installer => installer.Install(hostBuilder, configuration));

            return hostBuilder;
        }

    }
}
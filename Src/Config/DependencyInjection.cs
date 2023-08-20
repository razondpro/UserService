using System.Reflection;

namespace UserService.Config
{
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

    }
}
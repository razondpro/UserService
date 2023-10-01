using Serilog;

namespace UserService.Config.Logs
{
    public class SerilogHostInstaller : IHostInstaller
    {
        public void Install(IHostBuilder hostBuilder, IConfiguration configuration)
        {
            hostBuilder.UseSerilog((context, configuration) =>
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Filter.ByExcluding(e => e.Exception is FluentValidation.ValidationException)
            );
        }
    }
}
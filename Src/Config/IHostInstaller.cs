namespace UserService.Config
{
    public interface IHostInstaller
    {
        void Install(IHostBuilder hostBuilder, IConfiguration configuration);
    }
}
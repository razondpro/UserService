namespace UserService.Config.Validation
{
    using FluentValidation;
    using UserService.Modules.User.Application.CreateUser;
    using UserService.Modules.User.Application.GetUserByEmail;
    public class FluentValidationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
            services.AddScoped<IValidator<GetUserByEmailQuery>, GetUserByEmailValidator>();
        }
    }
}
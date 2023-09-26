namespace UserService.Config.Validation
{
    using FluentValidation;
    using UserService.Modules.User.Application.CreateUser;
    using UserService.Modules.User.Application.FindUserByEmail;
    using UserService.Modules.User.Application.UpdateUser;

    public class FluentValidationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<CreateUserRequestDto>, CreateUserValidator>();
            services.AddScoped<IValidator<FindUserByEmailRequestDto>, FindUserByEmailValidator>();
            services.AddScoped<IValidator<UpdateUserRequestDto>, UpdateUserValidator>();
        }
    }
}
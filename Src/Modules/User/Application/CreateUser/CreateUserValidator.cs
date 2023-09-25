namespace UserService.Modules.User.Application.CreateUser
{
    using FluentValidation;
    using UserService.Modules.User.Domain.ValueObjects;
    public class CreateUserValidator : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(Name.MinLength)
                .MaximumLength(Name.MaxLength)
                .Matches(Name.NameRegex);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(Name.MinLength)
                .MaximumLength(Name.MaxLength)
                .Matches(Name.NameRegex);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(Email.MaxLength)
                .Matches(Email.EmailRegex);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(UserName.MinLength)
                .MaximumLength(UserName.MaxLength)
                .Matches(UserName.UserNameRegex);
        }
    }
}

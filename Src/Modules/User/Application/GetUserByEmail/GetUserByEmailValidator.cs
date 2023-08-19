namespace UserService.Modules.User.Application.GetUserByEmail
{
    using FluentValidation;
    using UserService.Modules.User.Domain.ValueObjects;

    public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(Email.MaxLength)
                .Matches(Email.EmailRegex);
        }
    }

}
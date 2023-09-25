namespace UserService.Modules.User.Application.FindUserByEmail
{
    using FluentValidation;
    using UserService.Modules.User.Domain.ValueObjects;

    public class FindUserByEmailValidator : AbstractValidator<FindUserByEmailRequestDto>
    {
        public FindUserByEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(Email.MaxLength)
                .Matches(Email.EmailRegex);
        }
    }

}
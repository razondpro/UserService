using FluentValidation;
using UserService.Modules.User.Domain.ValueObjects;

namespace UserService.Modules.User.Application.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequestDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(UserName.MinLength)
                .MaximumLength(UserName.MaxLength)
                .Matches(UserName.UserNameRegex);

            When(x => !string.IsNullOrEmpty(x.FirstName), () =>
                RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .MinimumLength(Name.MinLength)
                    .MaximumLength(Name.MaxLength)
                    .Matches(Name.NameRegex)
            );

            When(x => !string.IsNullOrEmpty(x.LastName), () =>
                RuleFor(x => x.LastName)
                    .NotEmpty()
                    .MinimumLength(Name.MinLength)
                    .MaximumLength(Name.MaxLength)
                    .Matches(Name.NameRegex)
            );
        }

    }
}
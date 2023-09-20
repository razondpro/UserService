namespace UserService.Modules.User.Application.Behaviors
{
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using System.Linq;

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            var validationContext = new ValidationContext<TRequest>(request);

            var validationResults = await _validator.ValidateAsync(validationContext, cancellationToken);

            var errors = validationResults.Errors
            .Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage)).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
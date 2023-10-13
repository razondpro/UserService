using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UserService.Shared.Application.Core;
using UserService.Shared.Infrastructure.Http.Core;

namespace UserService.Modules.User.Application.CreateUser
{
    public class CreateUserHttpController :
        IHttpController<CreateUserRequestDto, Results<Created, Conflict<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContext;

        public CreateUserHttpController(IMediator mediator, IHttpContextAccessor httpContext)
        {
            this._mediator = mediator;
            this._httpContext = httpContext;
        }

        public async Task<Results<Created, Conflict<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(
             CreateUserRequestDto request,
             CancellationToken cancellationToken = default)
        {

            var result = await _mediator.Send(new CreateUserCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.UserName)
                , cancellationToken);

            return result.Match<Results<Created, Conflict<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.Created($"{_httpContext.HttpContext?.Request.Path}/{request.Email}"),
                Left: error => error switch
                {
                    EmailAlreadyExistsError => TypedResults.Conflict(
                        new ApiHttpErrorResponse(
                            "Conflict",
                            StatusCodes.Status409Conflict,
                            new List<ErrorDetail> { new("Email", "Email already exists") }
                            )
                        ),
                    UserNameAlreadyExistsError => TypedResults.Conflict(
                        new ApiHttpErrorResponse(
                            "Conflict",
                            StatusCodes.Status409Conflict,
                            new List<ErrorDetail> { new("UserName", "UserName already exists") }
                            )
                        ),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}
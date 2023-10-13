using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UserService.Shared.Application.Core;
using UserService.Shared.Infrastructure.Http.Core;

namespace UserService.Modules.User.Application.UpdateUser
{
    public class UpdateUserHttpController : IHttpController<
        UpdateUserRequestDto,
        Results<Ok, BadRequest<ApiHttpErrorResponse>,
        StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;

        public UpdateUserHttpController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public async Task<Results<Ok, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(UpdateUserRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new UpdateUserCommand(
                    request.UserName,
                    request.FirstName,
                    request.LastName)
                , cancellationToken);

            return result.Match<Results<Ok, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.Ok(),
                Left: error => error switch
                {
                    UserNotFoundError => TypedResults.BadRequest(
                        new ApiHttpErrorResponse(
                            "BadRequest",
                            StatusCodes.Status400BadRequest,
                            new List<ErrorDetail> { new("UserName", "User not found") }
                            )
                        ),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}
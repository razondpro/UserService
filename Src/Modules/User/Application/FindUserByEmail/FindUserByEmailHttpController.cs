using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UserService.Shared.Application.Core;
using UserService.Shared.Infrastructure.Http.Core;

namespace UserService.Modules.User.Application.FindUserByEmail
{
    public class FindUserByEmailHttpController :
        IHttpController<FindUserByEmailRequestDto, Results<Ok<FindUserByEmailHttpResponseDto>, NotFound, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;

        public FindUserByEmailHttpController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Results<Ok<FindUserByEmailHttpResponseDto>, NotFound, StatusCodeHttpResult>> Execute(
            FindUserByEmailRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FindUserByEmailQuery(request.Email), cancellationToken);

            return result.Match<Results<Ok<FindUserByEmailHttpResponseDto>, NotFound, StatusCodeHttpResult>>(
                Right: user => TypedResults.Ok(new FindUserByEmailHttpResponseDto(
                    "Ok",
                    StatusCodes.Status200OK,
                    new UserDto(user)
                    )
                ),
                Left: error => error switch
                {
                    UserNotFoundByEmailError => TypedResults.NotFound(),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}
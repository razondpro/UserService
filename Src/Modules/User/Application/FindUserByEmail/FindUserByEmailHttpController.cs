using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UserService.Shared.Application.Core;

namespace UserService.Modules.User.Application.FindUserByEmail
{
    public class FindUserByEmailHttpController : IController<FindUserByEmailRequestDto, Results<Ok<UserResponse>, NotFound, BadRequest<string>>>
    {
        private readonly IMediator _mediator;

        public FindUserByEmailHttpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Results<Ok<UserResponse>, NotFound, BadRequest<string>>> execute(FindUserByEmailRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {

                var result = await _mediator.Send(new FindUserByEmailQuery(request.Email), cancellationToken);

                return result.Match<Results<Ok<UserResponse>, NotFound, BadRequest<string>>>(
                    Right: user => TypedResults.Ok(user),
                    Left: error => error switch
                    {
                        UserNotFoundByEmailError => TypedResults.NotFound(),
                        _ => TypedResults.BadRequest("Unexpected error ocurred")
                    }
                );
            }
            catch (Exception)
            {
                return TypedResults.BadRequest("Unexpected error ocurred");
            }
        }
    }
}
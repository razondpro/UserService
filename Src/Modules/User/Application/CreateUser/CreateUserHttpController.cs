using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UserService.Shared.Application.Core;

namespace UserService.Modules.User.Application.CreateUser
{
    public class CreateUserHttpController : IController<CreateUserRequestDto, Results<Created, BadRequest<string>>>
    {
        private readonly IMediator _mediator;

        public CreateUserHttpController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<Results<Created, BadRequest<string>>> execute(CreateUserRequestDto request, CancellationToken cancellationToken = default)
        {

            try
            {
                var result = await _mediator.Send(new CreateUserCommand(
                        request.FirstName,
                        request.LastName,
                        request.Email,
                        request.UserName)
                    , cancellationToken);

                return result.Match<Results<Created, BadRequest<string>>>(
                    Right: _ => TypedResults.Created(request.UserName),
                    Left: error => error switch
                    {
                        EmailAlreadyExistsError => TypedResults.BadRequest("Email already exists"),
                        UserNameAlreadyExistsError => TypedResults.BadRequest("UserName already exists"),
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
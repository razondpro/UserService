namespace UserService.Modules.User.Application.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.HttpResults;
    using UserService.Modules.User.Application.Abstractions.Commands;
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Results<Ok, NotFound>>
    {
        private readonly IUserWriteRepository _userRepository;

        public CreateUserCommandHandler(IUserWriteRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Results<Ok, NotFound>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            var firstName = FirstName.Create(request.FirstName);
            var lastName = LastName.Create(request.LastName);
            var userName = UserName.Create(request.UserName);

            var user = User.Create(null, email, firstName, lastName, userName);

            await _userRepository.Create(user);

            return TypedResults.Ok();
        }
    }
}
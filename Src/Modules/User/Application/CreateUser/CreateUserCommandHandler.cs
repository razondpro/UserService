namespace UserService.Modules.User.Application.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.HttpResults;
    using UserService.Modules.User.Application.Abstractions.Commands;
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;



    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Results<Created, BadRequest<string>>>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public CreateUserCommandHandler(IUserWriteRepository userRepository, IUserReadRepository userReadRepository)
        {
            _userWriteRepository = userRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<Results<Created, BadRequest<string>>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            

            var email = Email.Create(request.Email);
            if (await _userReadRepository.Get(email) is not null)
            {
                return TypedResults.BadRequest("Email already exists");
            }

            var userName = UserName.Create(request.UserName);
            if (await _userReadRepository.Get(userName) is not null)
            {
                return TypedResults.BadRequest("Username already exists");
            }

            var name = Name.Create(request.FirstName, request.LastName);


            var user = User.Create(null, email, name, userName);

            await _userWriteRepository.Create(user);

            return TypedResults.Created(user.UserName.Value);
        }
    }
}
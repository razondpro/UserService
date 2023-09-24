namespace UserService.Modules.User.Application.CreateUser
{
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Modules.User.Domain.ValueObjects;
    using UserService.Shared.Application.Commands;
    using LanguageExt;

    public class CreateUserCommandHandler :
        ICommandHandler<CreateUserCommand, Either<Exception, Unit>>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        public CreateUserCommandHandler(
            IUserWriteRepository userRepository,
            IUserReadRepository userReadRepository
            )
        {
            _userWriteRepository = userRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<Either<Exception, Unit>> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            if (await _userReadRepository.Get(email) is not null)
            {
                return new EmailAlreadyExistsError();
            }

            var userName = UserName.Create(request.UserName);
            if (await _userReadRepository.Get(userName) is not null)
            {
                return new UserNameAlreadyExistsError();
            }

            var name = Name.Create(request.FirstName, request.LastName);


            var user = User.Create(null, email, name, userName);

            await _userWriteRepository.Create(user);


            return Unit.Default;
        }
    }
}
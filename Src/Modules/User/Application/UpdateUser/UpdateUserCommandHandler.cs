

namespace UserService.Modules.User.Application.UpdateUser
{
    using UserService.Modules.User.Domain.Entities;
    using LanguageExt;
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.ValueObjects;
    using UserService.Shared.Application.Commands;

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Either<Exception, Unit>>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public UpdateUserCommandHandler(
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository
        )
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task<Either<Exception, Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var userName = UserName.Create(request.UserName);

            var user = await _userReadRepository.Get(userName);

            if (user is null)
            {
                return new UserNotFoundError();
            }

            UpdateUser(user, request);

            await _userWriteRepository.Update(user);

            return Unit.Default;
        }


        private static void UpdateUser(User user, UpdateUserCommand request)
        {
            if (request.FirstName is not null)
            {
                user.Name.UpdateFirstName(request.FirstName);
            }

            if (request.LastName is not null)
            {
                user.Name.UpdateLastName(request.LastName);
            }
        }
    }
}
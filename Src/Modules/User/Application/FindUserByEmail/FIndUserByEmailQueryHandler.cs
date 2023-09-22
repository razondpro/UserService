namespace UserService.Modules.User.Application.FindUserByEmail
{
    using LanguageExt;
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.ValueObjects;
    using UserService.Shared.Application.Queries;

    public class FindUserByEmailQueryHandler : IQueryHandler<FindUserByEmailQuery, Either<Exception, UserResponse>>
    {
        private readonly IUserReadRepository _userRepository;

        public FindUserByEmailQueryHandler(IUserReadRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Either<Exception, UserResponse>> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(Email.Create(request.Email));

            if (user == null)
            {
                return new UserNotFoundByEmailError();
            }

            return new UserResponse(
                user.Name.FirstName,
                user.Name.LastName,
                user.Email.Value,
                user.UserName.Value
            );
        }
    }
}
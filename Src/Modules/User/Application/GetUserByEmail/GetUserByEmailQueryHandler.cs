namespace UserService.Modules.User.Application.GetUserByEmail
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.HttpResults;
    using UserService.Modules.User.Application.Abstractions.Queries;
    using UserService.Modules.User.Domain.Repositories;
    using UserService.Modules.User.Domain.ValueObjects;

    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, Results<Ok<UserResponse>, NotFound>>
    {
        private readonly IUserReadRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserReadRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Results<Ok<UserResponse>, NotFound>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(Email.Create(request.Email));

            if (user == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new UserResponse(
                user.Id.Value.ToString(),
                user.Name.FirstName,
                user.Name.LastName,
                user.Email.Value,
                user.UserName.Value
            ));
        }
    }
}
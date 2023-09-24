namespace UserService.Modules.User.Application.FindUserByEmail
{
    using System.Collections.Generic;
    using UserService.Modules.User.Domain.Entities;
    using UserService.Shared.Infrastructure.Http.Core;

    public class FindUserByEmailHttpResponseDto : ApiHttpResponse
    {
        public UserDto User { get; init; }

        public FindUserByEmailHttpResponseDto(string title, int status, UserDto user) : base(title, status)
        {
            User = user;
        }
    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; init; }
        public string Email { get; init; }

        public UserDto(User user)
        {
            FirstName = user.Name.FirstName;
            LastName = user.Name.LastName;
            UserName = user.UserName.Value;
            Email = user.Email.Value;
        }
    }
}
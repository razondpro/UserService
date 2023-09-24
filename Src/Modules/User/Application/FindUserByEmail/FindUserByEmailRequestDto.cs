using Microsoft.AspNetCore.Mvc;

namespace UserService.Modules.User.Application.FindUserByEmail
{
    public record FindUserByEmailRequestDto([FromRoute(Name = "email")] string Email);
}
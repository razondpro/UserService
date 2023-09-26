using Microsoft.AspNetCore.Mvc;

namespace UserService.Modules.User.Application.FindUserByEmail
{
    public sealed record FindUserByEmailRequestDto([FromRoute(Name = "email")] string Email);
}
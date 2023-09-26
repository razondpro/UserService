using UserService.Shared.Application.Exceptions;

namespace UserService.Modules.User.Application.UpdateUser
{
    public class UserNotFoundError : ApplicationError
    {
        public const string DefaultMessage = "User not found";
        public UserNotFoundError() : base(DefaultMessage)
        {
        }
    }
}
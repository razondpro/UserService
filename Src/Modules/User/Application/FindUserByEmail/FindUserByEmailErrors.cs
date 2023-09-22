using UserService.Shared.Application.Exceptions;

namespace UserService.Modules.User.Application.FindUserByEmail
{
    public class UserNotFoundByEmailError : ApplicationError
    {
        private const string DefaultMessage = "User not found by email";
        public UserNotFoundByEmailError() : base(DefaultMessage)
        {
        }
    }
}
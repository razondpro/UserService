namespace UserService.Modules.User.Application.CreateUser
{
    using UserService.Shared.Application.Exceptions;

    public class EmailAlreadyExistsError : ApplicationError
    {
        private const string DefaultMessage = "Email already exists";
        public EmailAlreadyExistsError() : base(DefaultMessage)
        {
        }
    }

    public class UserNameAlreadyExistsError : ApplicationError
    {
        private const string DefaultMessage = "Username already exists";
        public UserNameAlreadyExistsError() : base(DefaultMessage)
        {
        }
    }
}
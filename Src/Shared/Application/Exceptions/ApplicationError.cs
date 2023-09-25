namespace UserService.Shared.Application.Exceptions
{
    public class ApplicationError : Exception
    {
        private const string DefaultMessage = "An Application error has occurred";
        public ApplicationError(string message) : base(message ?? DefaultMessage)

        {
        }

    }
}
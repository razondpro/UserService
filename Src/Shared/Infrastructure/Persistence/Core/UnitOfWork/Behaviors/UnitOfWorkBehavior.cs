using MediatR;
namespace UserService.Shared.Infrastructure.Persistence.Core.UnitOfWork.Behaviors
{
    using UserService.Shared.Infrastructure.Persistence.Core.UnitOfWork;
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {

        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!typeof(TRequest).Name.EndsWith("Command"))
            {
                return await next();
            }

            var response = await next();
            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}
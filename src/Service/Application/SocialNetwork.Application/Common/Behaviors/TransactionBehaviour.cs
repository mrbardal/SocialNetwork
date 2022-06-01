using MediatR;
using SocialNetwork.Application.Common.Behaviors.Core;
using SocialNetwork.Infrastructure.Persistance.Core;

namespace SocialNetwork.Application.Common.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(IUnitOfWork));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            if (request is IUnitOfWorkRequest)
            {
                await _unitOfWork.BeginAsync();
                var response = await next();
                await _unitOfWork.SaveAsync();

                return response;
            }
            else
            {
                return await next();
            }
        }
        catch
        {
            //if (request is IUnitOfWorkRequest)
            //{
            //    //await _context.RollbackAsync();
            //}

            throw;
        }
    }
}

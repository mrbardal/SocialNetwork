using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using SocialNetwork.Application.Common.Exceptions;

namespace SocialNetwork.Application.Common.Behaviors;

public class ExceptionBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;

    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            if (typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var responseType = typeof(Result<>).MakeGenericType(resultType);
                var x = typeof(Result)
                            .GetMethods()
                            .Where(m => m.Name == nameof(Result.Failure) && m.IsGenericMethod)
                            .FirstOrDefault()?
                            .MakeGenericMethod(resultType)
                            .Invoke(this, new[] { e.Message });

                return (TResponse)x!;
            }

            throw new AppException($"Errors occured in {typeof(TRequest)}", e);
        }
    }
}

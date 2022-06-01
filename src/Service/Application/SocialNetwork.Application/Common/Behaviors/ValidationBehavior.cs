using FluentValidation;
using MediatR;
using SocialNetwork.Application.Common.Exceptions;
using System.Text;

namespace SocialNetwork.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public Task<TResponse> Handle(
        TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            var errorBuilder = new StringBuilder();

            errorBuilder.AppendLine($"{typeof(TRequest)} Validation Errors :");
            failures.ForEach(error => errorBuilder.AppendLine(error.ErrorMessage));

            throw new AppException(errorBuilder.ToString());
        }

        return next();
    }
}

using CSharpFunctionalExtensions;
using MediatR;

namespace SocialNetwork.Application.Featuers.UserFeature.Commands.Register;

public record RegisterUserCommand :
        IRequest<Result<RegisterUserCommandResult>>//, IUnitOfWorkRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }

    public RegisterUserCommand(string userName, string password) => (UserName, Password) = (userName, password);
}

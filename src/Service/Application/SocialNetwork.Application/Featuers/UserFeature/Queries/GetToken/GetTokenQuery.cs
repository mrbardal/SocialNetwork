using CSharpFunctionalExtensions;
using MediatR;

namespace SocialNetwork.Application.Featuers.UserFeature.Queries.GetToken;

public record GetTokenQuery :
    IRequest<Result<GetTokenQueryResult>>
{
    public string UserName { get; init; }
    public string Password { get; init; }

    public GetTokenQuery(string userName, string password) => (UserName, Password) = (userName, password);

}

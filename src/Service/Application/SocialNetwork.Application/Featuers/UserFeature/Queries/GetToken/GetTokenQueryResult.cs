namespace SocialNetwork.Application.Featuers.UserFeature.Queries.GetToken;

public record GetTokenQueryResult
{
    public string AccessToken { get; init; }
    public string UserName { get; init; }

    public GetTokenQueryResult(string userName, string accessToken)
        => (UserName, AccessToken) = (userName, accessToken);
}

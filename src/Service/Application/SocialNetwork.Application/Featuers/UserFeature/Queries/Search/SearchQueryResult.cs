namespace SocialNetwork.Application.Featuers.UserFeature.Queries.Search;

public record SearchQueryResult
{
    public List<string> UserNames { get; init; }
    public SearchQueryResult(List<string> userNames)
        => (UserNames) = (userNames);
}

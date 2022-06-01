using CSharpFunctionalExtensions;
using MediatR;

namespace SocialNetwork.Application.Featuers.UserFeature.Queries.Search;

public record SearchQuery :
    IRequest<Result<SearchQueryResult>>
{
    public string Name { get; init; }

    public SearchQuery(string name) => (Name) = (name);

}

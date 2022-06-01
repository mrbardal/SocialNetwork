using SocialNetwork.Application.Models;
using MediatR;

namespace SocialNetwork.Application.Featuers.ProductFeature.Queries.GetById;

public record GetProductByIdQuery :
    IRequest<Result<GetProductByIdQueryResult>>
{
    public Guid Id { get; init; }

    public GetProductByIdQuery(Guid id) => Id = id;
}

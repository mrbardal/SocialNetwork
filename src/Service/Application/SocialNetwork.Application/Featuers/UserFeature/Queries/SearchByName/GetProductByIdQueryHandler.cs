using AutoMapper;
using SocialNetwork.Application.Models;
using SocialNetwork.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace SocialNetwork.Application.Featuers.ProductFeature.Queries.GetById;

public class GetProductByIdQueryHandler :
    IRequestHandler<GetProductByIdQuery, Result<GetProductByIdQueryResult>>
{
    private IProductRepository _repository;
    private IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductByIdQueryResult>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProductByIdAsync(request.Id);
        var result = _mapper.Map<GetProductByIdQueryResult>(product);

        return Result.Success(result);
    }
}

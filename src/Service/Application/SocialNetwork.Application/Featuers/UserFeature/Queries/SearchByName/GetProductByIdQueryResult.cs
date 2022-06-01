namespace SocialNetwork.Application.Featuers.ProductFeature.Queries.GetById;

public record GetProductByIdQueryResult
{
    public Guid Id { get; }
    public string Name { get; }
    public int Price { get; }

    public GetProductByIdQueryResult(Guid id, string name, int price)
        => (Id, Name, Price) = (id, name, price);
}

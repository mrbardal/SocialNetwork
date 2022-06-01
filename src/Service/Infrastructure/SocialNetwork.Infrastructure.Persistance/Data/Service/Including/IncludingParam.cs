using System.Linq.Expressions;

namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Including;

public class IncludingParam<T>
{
    private readonly List<Expression<Func<T, object>>> _includes;

    public IReadOnlyCollection<Expression<Func<T, object>>> Includes => _includes;

    public IncludingParam()
    {
        _includes = new List<Expression<Func<T, object>>>();
    }

    public void AddInclude(Expression<Func<T, object>> field)
    {
        _includes.Add(field);
    }
}
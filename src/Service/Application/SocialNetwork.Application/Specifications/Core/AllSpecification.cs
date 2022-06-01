using System.Linq.Expressions;

namespace SocialNetwork.Application.Specifications.Core;

public sealed class AllSpecification<T> : Specification<T>
{
    public AllSpecification()
    {
    }

    public override Expression<Func<T, bool>> AsExpression() => t => true;
}

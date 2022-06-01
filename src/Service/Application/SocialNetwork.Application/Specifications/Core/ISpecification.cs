using System.Linq.Expressions;

namespace SocialNetwork.Application.Specifications.Core;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T candidate);
    Expression<Func<T, bool>> AsExpression();
    Specification<T> And(Specification<T> other);
    Specification<T> Or(Specification<T> other);
    Specification<T> Not();
}

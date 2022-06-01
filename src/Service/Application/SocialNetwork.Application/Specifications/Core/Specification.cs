using System.Linq.Expressions;

namespace SocialNetwork.Application.Specifications.Core;

public abstract class Specification<T> : ISpecification<T>
{
    public static readonly Specification<T> All = new AllSpecification<T>();
    public abstract Expression<Func<T, bool>> AsExpression();
    public bool IsSatisfiedBy(T entity) => AsExpression().Compile().Invoke(entity);

    public Specification<T> And(Specification<T> other)
    {
        if (this == All)
            return other;
        if (other == All)
            return this;

        return new AndSpecification<T>(this, other);
    }

    public Specification<T> Or(Specification<T> other)
    {
        if (this == All || other == All)
            return All;

        return new OrSpecification<T>(this, other);
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}

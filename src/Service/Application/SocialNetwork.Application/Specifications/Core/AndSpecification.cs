using System.Linq.Expressions;

namespace SocialNetwork.Application.Specifications.Core;

public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> AsExpression()
    {
        var expLeft = _left.AsExpression();
        var expRight = _right.AsExpression();
        var and = Expression.AndAlso(expLeft.Body, Expression.Invoke(expRight, expLeft.Parameters));

        return Expression.Lambda<Func<T, bool>>(and, expLeft.Parameters);
    }
}

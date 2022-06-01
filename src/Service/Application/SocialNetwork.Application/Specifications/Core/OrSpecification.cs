using System.Linq.Expressions;

namespace SocialNetwork.Application.Specifications.Core;

public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> AsExpression()
    {
        var expLeft = _left.AsExpression();
        var expRight = _right.AsExpression();
        BinaryExpression orExpression = Expression.OrElse(expLeft.Body, expRight.Body);

        return Expression.Lambda<Func<T, bool>>(orExpression, expLeft.Parameters.Single());
    }
}

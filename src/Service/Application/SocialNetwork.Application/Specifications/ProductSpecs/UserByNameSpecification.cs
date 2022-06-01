using SocialNetwork.Application.Specifications.Core;
using SocialNetwork.Infrastructure.Identity;
using System.Linq.Expressions;

namespace SocialNetwork.Application.Specifications.UserSpecs;

public class UserByNameSpecification : Specification<AppUser>
{
    private string _name;

    public UserByNameSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<AppUser, bool>> AsExpression()
    {
        return u => u.UserName.Contains(_name);
    }
}

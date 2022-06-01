using System.Linq.Expressions;

namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Sorting;

public class SortData<T>
{
    public Expression<Func<T, object>> Field { get; set; }
    public SortDirection Direction { get; set; }
}
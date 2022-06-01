using System.Linq.Expressions;

namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Sorting;

public class SortingParam<T>
{
    private readonly List<SortData<T>> _sorts;

    public IReadOnlyCollection<SortData<T>> Sorts => _sorts;

    public SortingParam()
    {
        _sorts = new List<SortData<T>>();
    }

    public void AddSort(Expression<Func<T, object>> field, SortDirection direction)
    {
        _sorts.Add(new SortData<T>() { Field = field, Direction = direction });
    }
}
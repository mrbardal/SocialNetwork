using SocialNetwork.Infrastructure.Persistance.Data.Service.Sorting;
using System.Linq.Expressions;

namespace SocialNetwork.Application.Queries;

public static class SortingExtension
{
    public static SortingParam<T>? Sorting<T>(this string sorting)
    {
        if (string.IsNullOrEmpty(sorting)) return default;

        SortingParam<T>? sortingParam = new SortingParam<T>();

        sortingParam = new SortingParam<T>();

        string[] sorts = sorting.Split(',');

        sorts.ToList().ForEach(sort =>
        {
            string[] sortItem = sort.Split(' ');
            var field = sortItem[0];
            var direction = sortItem[1].Equals("asc") ? SortDirection.Ascending : SortDirection.Descending;

            var type = typeof(T);
            ParameterExpression pe = Expression.Parameter(type, type.Name.ToLower());
            var prop = type.GetProperty(field);

            if (prop != null)
            {
                MemberExpression propExp = Expression.Property(pe, prop!);
                Expression conversion = Expression.Convert(propExp, typeof(object));
                var exp = Expression.Lambda<Func<T, object>>(conversion, new[] { pe });
                sortingParam.AddSort(exp, direction);
            }
        });

        return sortingParam;
    }
}

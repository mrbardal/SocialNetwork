using Microsoft.EntityFrameworkCore;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Including;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Paging;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Sorting;
using System.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace SocialNetwork.Infrastructure.Persistance.Data.Service;

public static class DbExtensions
{
    private static IOrderedQueryable<T> Sort<T>(IQueryable<T> source, SortData<T> sorting)
    {
        return sorting.Direction switch
        {
            SortDirection.Ascending => source.OrderBy(sorting.Field),
            SortDirection.Descending => source.OrderByDescending(sorting.Field),
            _ => (IOrderedQueryable<T>)source
        };
    }
    private static IOrderedQueryable<T> Sort<T>(IOrderedQueryable<T> source, SortData<T> sorting)
    {
        return sorting.Direction switch
        {
            SortDirection.Ascending => source.ThenBy(sorting.Field),
            SortDirection.Descending => source.ThenByDescending(sorting.Field),
            _ => (IOrderedQueryable<T>)source
        };
    }

    public static async Task<PagedList<T>> Paging<T>(this IQueryable<T> query, PagingParam? paging = default)
    {
        if (paging == null)
        {
            paging = new PagingParam();
        }

        var totalCount = await query.CountAsync();
        var result = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
            .Take(paging.PageSize).ToListAsync();

        return new PagedList<T>(result, totalCount, paging.PageIndex, paging.PageSize);
    }

    public static IQueryable<T> Tracking<T>(this IQueryable<T> query, bool tracking)
        where T : class
    {
        return tracking ? query : query.AsNoTracking();
    }

    public static IQueryable<T> Sorting<T>(this IQueryable<T> query, SortingParam<T>? sorting = default)
    {
        if (sorting == null || !sorting.Sorts.Any())
        {
            return query;
        }
        else if (sorting.Sorts.Count == 1)
        {
            return Sort(query, sorting.Sorts.First());
        }
        else
        {
            var sortedList = Sort(query, sorting.Sorts.First());

            sorting.Sorts.ToList().GetRange(1, sorting.Sorts.Count - 1).ForEach(sort =>
            {
                sortedList = Sort(sortedList, sort);
            });

            return sortedList;
        }
    }
    public static IQueryable<T> Sorting<T>(this IQueryable<T> query, string? sorting = default)
    {
        if (string.IsNullOrEmpty(sorting))
        {
            return query;
        }

        return query.OrderBy(sorting);
    }

    public static IQueryable<T> Searching<T>(this IQueryable<T> set, string? searchText = default)
    {
        if (string.IsNullOrEmpty(searchText)) return set;

        var type = typeof(T);

        var props = type.GetProperties()
                        .Where(prop => prop.PropertyType.Name.Equals("String"))
                        .ToList();

        if (!props.Any()) return set;

        List<Expression> expressions = new List<Expression>();
        ParameterExpression parameter = Expression.Parameter(type);
        MethodInfo? miContains = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        ConstantExpression ceType = Expression.Constant(searchText, typeof(string));

        props.ForEach(prop =>
        {
            MemberExpression meType = Expression.Property(parameter, prop);
            MethodCallExpression callExpression = Expression.Call(meType, miContains!, ceType);

            expressions.Add(callExpression);
        });

        if (expressions.Count == 1)
        {
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(expressions.First(), new[] { parameter });

            return set.Where(lambda);
        }
        else
        {
            BinaryExpression beBody = Expression.Or(expressions[0], expressions[1]);

            for (int i = 2; i < expressions.Count; i++)
            {
                beBody = Expression.Or(beBody, expressions[i]);
            }

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(beBody, new[] { parameter });

            return set.Where(lambda);
        }
    }

    public static IEnumerable<dynamic> Shaping<T>(this IEnumerable<T> query, string? fields = default)
        where T : class
    {
        if (string.IsNullOrEmpty(fields))
        {
            return query;
        }

        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var fieldNames = fields.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        return query.Select(entity =>
        {
            var shaped = new ExpandoObject();

            fieldNames.ForEach(fieldName =>
            {
                var propertyInfo = propertyInfos.FirstOrDefault(pi =>
                        pi.Name.Equals(fieldName.Trim(), StringComparison.OrdinalIgnoreCase));

                if (propertyInfo == null) return;

                var pValue = propertyInfo.GetValue(entity);
                shaped.TryAdd(propertyInfo.Name, pValue);
            });

            return shaped;
        });
    }

    public static IQueryable<T> Query<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
    {
        return predicate != null ? query.Where(predicate) : query;
    }
    public static IQueryable<T> Query<T>(this IQueryable<T> query, string predicate)
    {
        return predicate != null ? query.Where(predicate) : query;
    }

    public static IQueryable<T> Including<T>(this IQueryable<T> query, string? including = default)
        where T : class
    {
        if (string.IsNullOrEmpty(including))
            return query;


        including.Split(',').ToList().ForEach(include => query.Include(include));

        return query;
    }
    public static IQueryable<T> Including<T>(this IQueryable<T> query, IncludingParam<T>? including = default)
        where T : class
    {
        if (including == null || !including.Includes.Any())
            return query;


        including.Includes.ToList().ForEach(include => query.Include(include));

        return query;
    }
}

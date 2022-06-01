using SocialNetwork.Domain.Core;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Including;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Paging;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Sorting;
using System.Linq.Expressions;

namespace SocialNetwork.Infrastructure.Persistance.Core;

public interface IRepository<T, in TKey>
where T : IEntity<TKey>, IAggregateRoot
where TKey : IEquatable<TKey>, IComparable<TKey>
{
    #region CRUD
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(TKey id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    #endregion

    #region GetEntities
    Task<PagedList<T>> GetEntitiesAsync(
        Expression<Func<T, bool>> predicate,
        PagingParam? paging = default(PagingParam),
        SortingParam<T>? sorting = default(SortingParam<T>),
        IncludingParam<T>? including = default(IncludingParam<T>),
        bool tracking = false);
    Task<PagedList<T>> GetEntitiesAsync(
        string predicate,
        PagingParam? paging = default,
        string? sorting = default,
        string? including = default,
        bool tracking = false);
    #endregion

    #region Filter
    Task<PagedList<T>> FilterAsync(
        Expression<Func<T, bool>> predicate,
        PagingParam? paging = default,
        SortingParam<T>? sorting = default,
        IncludingParam<T>? including = default);
    Task<PagedList<T>> FilterAsync(
        string predicate,
        PagingParam? paging = default,
        string? sorting = default,
        string? including = default);

    #endregion

    #region Search
    Task<PagedList<T>> SearchAsync(
        string searchText,
        PagingParam? paging = default,
        SortingParam<T>? sorting = default,
        IncludingParam<T>? including = default);
    Task<PagedList<T>> SearchAsync(
        string searchText,
        PagingParam? paging = default,
        string? sorting = default,
        string? including = default);
    #endregion
}

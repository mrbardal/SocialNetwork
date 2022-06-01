using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Core;
using SocialNetwork.Infrastructure.Persistance.Core;
using SocialNetwork.Infrastructure.Persistance.Data.Service;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Including;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Paging;
using SocialNetwork.Infrastructure.Persistance.Data.Service.Sorting;
using System.Linq.Expressions;

namespace SocialNetwork.Infrastructure.Persistance.Repositories.Core;

public class Repository<T, TKey> : IRepository<T, TKey>
where T : Entity<TKey>, IAggregateRoot
where TKey : IEquatable<TKey>, IComparable<TKey>
{
    #region Members
    protected readonly AppContextDb _context;
    protected readonly DbSet<T> _set;

    public Repository(AppContextDb context)
    {
        _context = context;
        _set = _context.Set<T>() ?? throw new ArgumentException($"DbSet of {typeof(T).Name} not defined in DbContext");
    }
    #endregion

    #region CRUD
    public virtual async Task<T?> GetByIdAsync(TKey id)
    {
        return await _set.FindAsync(id);
    }
    public async Task AddAsync(T entity)
    {
        await _set.AddAsync(entity);
    }
    public async Task UpdateAsync(T entity)
    {
        await Task.Run(() => _context.Entry<T>(entity).State = EntityState.Modified);
    }
    public async Task DeleteAsync(T entity)
    {
        await Task.Run(() => _set.Remove(entity));
    }
    #endregion

    #region GetEntitties
    public Task<PagedList<T>> GetEntitiesAsync(
        Expression<Func<T, bool>> predicate,
        PagingParam? paging = default,
        SortingParam<T>? sorting = default,
        IncludingParam<T>? including = default,
        bool tracking = false)
    {
        return _set
                .Query(predicate)
                .Tracking(tracking)
                .Sorting(sorting)
                .Including(including)
                .Paging(paging);
    }
    public Task<PagedList<T>> GetEntitiesAsync(
        string predicate,
        PagingParam? paging = default,
        string? sorting = default,
        string? including = default,
        bool tracking = false)
    {
        return _set
                .Query(predicate)
                .Tracking(tracking)
                .Sorting(sorting)
                .Including(including)
                .Paging(paging);
    }
    #endregion

    #region Filter
    public Task<PagedList<T>> FilterAsync(
        Expression<Func<T, bool>> predicate,
        PagingParam? paging = default,
        SortingParam<T>? sorting = default,
        IncludingParam<T>? including = default)
    {
        return _set
                .Query(predicate)
                .Tracking(false)
                .Sorting(sorting)
                .Including(including)
                .Paging(paging);
    }
    public Task<PagedList<T>> FilterAsync(
        string predicate,
        PagingParam? paging = null,
        string? sorting = null,
        string? including = null)

    {
        return _set
                .Query(predicate)
                .Tracking(false)
                .Sorting(sorting)
                .Including(including)
                .Paging(paging);
    }
    #endregion

    #region Search
    public Task<PagedList<T>> SearchAsync(
        string searchText,
        PagingParam? paging = default,
        SortingParam<T>? sorting = default,
        IncludingParam<T>? including = default)
    {
        return _set
                .Searching(searchText)
                .Tracking(false)
                .Sorting(sorting)
                .Including(including)
                .Paging(paging);
    }
    public Task<PagedList<T>> SearchAsync(
        string searchText,
        PagingParam? paging = default,
        string? sorting = default,
        string? including = default)
    {
        return _set
                .Searching(searchText)
                .Tracking(false)
                .Sorting(sorting)
                .Including(including)
                .Paging(paging);
    }
    #endregion
}
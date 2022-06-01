using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SocialNetwork.Infrastructure.Persistance.Core;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task SaveAsync();
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();
}

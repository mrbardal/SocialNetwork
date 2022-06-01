namespace SocialNetwork.Infrastructure.Persistance.Core;

public interface IUnitOfWork
{
    Task SaveAsync();
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();
}

using Microsoft.EntityFrameworkCore;
using SocialNetwork.Infrastructure.Persistance.Core;
using System.Data;

namespace SocialNetwork.Infrastructure.Persistance.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppContextDb _context;

    public UnitOfWork(AppContextDb context)
    {
        _context = context;
    }

    //public Task SaveAsync()=> _context.SaveAsync();

    //public Task BeginAsync()=> _context.BeginAsync();

    //public Task CommitAsync() => _context.CommitAsync();

    //public Task RollbackAsync() => _context.RollbackAsync();

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task BeginAsync()
    {
        await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        if (_context.Database.CurrentTransaction != null)
        {
            await _context.Database.CurrentTransaction.CommitAsync();
        }
    }

    public async Task RollbackAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            await _context.Database.CurrentTransaction.RollbackAsync();
        }
    }

}

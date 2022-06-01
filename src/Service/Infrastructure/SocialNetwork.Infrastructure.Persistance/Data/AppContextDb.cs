using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Aggregates.FriendshipAggregate;
using SocialNetwork.Infrastructure.Identity;

namespace SocialNetwork.Infrastructure.Persistance;

public class AppContextDb : IdentityDbContext<AppUser, AppRole, int>
{
    public const string DatabaseSchema = "SocialNetwork";

    public AppContextDb(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new AccountConfig());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContextDb).Assembly);
        modelBuilder.Entity<FriendshipStatus>().HasData(FriendshipStatus.GetAll<FriendshipStatus>());
        modelBuilder.Entity<Friendship>().HasOne<FriendshipStatus>().WithMany().HasForeignKey(p => p.StatusId);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Friendship> Friendships { get; set; }
}

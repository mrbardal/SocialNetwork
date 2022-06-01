using SocialNetwork.Domain.Aggregates.FriendshipAggregate;
using SocialNetwork.Infrastructure.Persistance.Repositories.Core;

namespace SocialNetwork.Infrastructure.Persistance.Repositories;

public class FriendshipRepository : Repository<Friendship, int>, IFriendshipRepository
{
    public FriendshipRepository(AppContextDb context) : base(context)
    {
    }

    public async Task AddFriendshipAsync(Friendship Friendship)
    {
        await AddAsync(Friendship);
    }

    public async Task<IEnumerable<string>> GetFollowersByUserNameAsync(string addressee)
    {
        var result = await FilterAsync(f => f.Addressee == addressee && f.StatusId == FriendshipStatus.Confirmed.Id);

        return result.Select(f => f.Requester);
    }

    public async Task<IEnumerable<string>> GetFollowingsByUserNameAsync(string requester)
    {
        var result = await FilterAsync(f => f.Requester == requester && f.StatusId == FriendshipStatus.Confirmed.Id);

        return result.Select(f => f.Addressee);
    }

    public async Task<Friendship?> GetFriendshipAsync(string requester, string addressee)
    {
        var result = await FilterAsync(f => f.Requester == requester && f.Addressee == addressee);

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<string>> GetFriendshipRequestsByUserNameAsync(string addressee)
    {
        var result = await FilterAsync(f => f.Addressee == addressee && f.StatusId == FriendshipStatus.Requested.Id);

        return result.Select(f => f.Requester);
    }

    public async Task UpdateFriendshipAsync(Friendship Friendship)
    {
        await UpdateAsync(Friendship);
    }
}

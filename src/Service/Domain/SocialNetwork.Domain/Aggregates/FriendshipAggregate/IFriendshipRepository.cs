
namespace SocialNetwork.Domain.Aggregates.FriendshipAggregate
{
    public interface IFriendshipRepository
    {
        Task AddFriendshipAsync(Friendship Friendship);
        Task UpdateFriendshipAsync(Friendship Friendship);
        Task<Friendship?> GetFriendshipAsync(string requester, string addressee);
        Task<IEnumerable<string>> GetFriendshipRequestsByUserNameAsync(string addressee);
        Task<IEnumerable<string>> GetFollowersByUserNameAsync(string addressee);
        Task<IEnumerable<string>> GetFollowingsByUserNameAsync(string requester);
    }
}

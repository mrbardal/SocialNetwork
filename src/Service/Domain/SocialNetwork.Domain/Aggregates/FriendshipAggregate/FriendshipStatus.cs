using SocialNetwork.Domain.Core;

namespace SocialNetwork.Domain.Aggregates.FriendshipAggregate;

public class FriendshipStatus : Enumeration
{
    public static FriendshipStatus Requested = new FriendshipStatus(1, nameof(Requested).ToLowerInvariant());
    public static FriendshipStatus Confirmed = new FriendshipStatus(2, nameof(Confirmed).ToLowerInvariant());
    public static FriendshipStatus Rejected = new FriendshipStatus(3, nameof(Rejected).ToLowerInvariant());

    public FriendshipStatus(int id, string name)
        : base(id, name)
    {
    }
}

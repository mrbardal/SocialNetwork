using SocialNetwork.Domain.Core;

namespace SocialNetwork.Domain.Aggregates.FriendshipAggregate;

public class Friendship : Entity<int>, IAggregateRoot
{
    public string Requester { get; protected set; }
    public string Addressee { get; protected set; }
    //public FriendshipStatus Status { get; protected set; }
    public int StatusId { get; set; }
    //private int _friendshipStatusId;

    public Friendship()
    {

    }
}
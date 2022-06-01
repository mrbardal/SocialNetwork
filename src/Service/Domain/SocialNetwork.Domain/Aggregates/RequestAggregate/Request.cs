using SocialNetwork.Domain.Core;

namespace SocialNetwork.Domain.Aggregates.RequestAggregate;

public class Request : Entity<int>, IAggregateRoot
{
    public int SenderId { get; protected set; }
    public int FriendId { get; protected set; }
    public RequestStatus Status { get; protected set; }
    private int _orderStatusId;

    public Request()
    {

    }
}
using SocialNetwork.Domain.Core;

namespace SocialNetwork.Domain.Aggregates.RequestAggregate;

public class RequestStatus : Enumeration
{
    public static RequestStatus Submitted = new RequestStatus(1, nameof(Submitted).ToLowerInvariant());
    public static RequestStatus Confirmed = new RequestStatus(2, nameof(Confirmed).ToLowerInvariant());
    public static RequestStatus Rejected = new RequestStatus(3, nameof(Rejected).ToLowerInvariant());

    public RequestStatus(int id, string name)
        : base(id, name)
    {
    }
}

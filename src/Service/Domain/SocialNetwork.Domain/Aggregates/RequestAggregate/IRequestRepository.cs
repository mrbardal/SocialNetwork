
namespace SocialNetwork.Domain.Aggregates.RequestAggregate
{
    public interface IRequestRepository
    {
        Task AddRequestAsync(Request Request);
        Task UpdateRequestAsync(Request Request);
        Task<Request?> GetRequestByIdAsync(int id);

    }
}

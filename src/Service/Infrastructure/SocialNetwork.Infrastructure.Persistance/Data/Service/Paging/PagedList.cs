namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Paging;

public class PagedList<T> : List<T>
{
    public PagingData Paging { get; }

    public PagedList(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
    {
        AddRange(items);
        Paging = new PagingData(totalCount, pageIndex, pageSize);
    }
}

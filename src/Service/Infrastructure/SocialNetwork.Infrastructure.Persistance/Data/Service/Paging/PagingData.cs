namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Paging;

public class PagingData
{
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagingData(int totalCount, int pageIndex, int pageSize)
    {
        TotalCount = totalCount;
        CurrentPage = pageIndex;
        PageSize = pageSize;
    }
}

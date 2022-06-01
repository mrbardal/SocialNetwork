namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Paging;

public class PagingParam
{
    private const int MaxPageSize = 50;
    private const int DefaultPageSize = 10;

    public int PageIndex { get; set; } = 1;
    private int _pageSize = DefaultPageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize ? MaxPageSize : value);
    }
}

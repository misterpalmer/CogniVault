namespace CogniVault.Platform.Core.Collections;

public class PagedListQueryStringParameters
{
    private const int MaxPageSize = 50;

    private int _pageSize = 10;
    public int PageNumber { get; set; } = 0;

    public int PageSize
    {
        get => _pageSize;

        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}
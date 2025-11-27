namespace CleanArchitecture.Application.DTOs.Common;

/// <summary>
/// Base pagination request for queries
/// </summary>
public record PaginationRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public PaginationRequest()
    {
    }

    public PaginationRequest(int page, int pageSize)
    {
        Page = page > 0 ? page : 1;
        PageSize = pageSize is > 0 and <= 100 ? pageSize : 10;
    }
}

/// <summary>
/// Generic paginated response
/// </summary>
public record PagedResponse<T>
{
    public List<T> Items { get; init; } = new();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public PagedResponse()
    {
    }

    public PagedResponse(List<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }
}

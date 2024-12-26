namespace Task_EMCO.Server.Common;

public class PaginatedResult<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PaginatedResult(IEnumerable<T> data, int totalCount, int pageNumber, int pageSize)
    {
        Data = data;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
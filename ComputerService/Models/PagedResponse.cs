namespace ComputerService.Models;
public class PagedResponse<T> : Response<T>
{
    public IEnumerable<Link> Links { get; set; }
    public Metadata Metadata { get; set; } = new();

    public PagedResponse(T data, int currentPage, int pageSize, int totalPages, int totalCount)
    {
        Metadata.CurrentPage = currentPage;
        Metadata.PageSize = pageSize;
        Metadata.TotalPages = totalPages;
        Metadata.TotalCount = totalCount;
        Data = data;
        Succeeded = true;
        ErrorMessage = null;
    }
}


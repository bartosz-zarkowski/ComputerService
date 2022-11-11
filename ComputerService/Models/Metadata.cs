namespace ComputerService.Models;

public class Metadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public Metadata()
    {

    }

    public Metadata(int currentPage, int pageSize, int totalPages, int totalCount)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalCount = totalCount;
    }
}
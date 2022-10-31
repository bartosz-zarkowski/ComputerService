using ComputerService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Models;

public class PagedList<T> : List<T>
{
    public PagedList()
    {
    }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);
        if (pageNumber > (int)Math.Ceiling(count / (double)pageSize) && count != 0)
        {
            throw new BadRequestException("Page number must be lesser or equal to " + totalPages + ".");
        }

        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
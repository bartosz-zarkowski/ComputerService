using AutoMapper;
using ComputerService.Interfaces;
using ComputerService.Models;

namespace ComputerService.Services;

public class PaginationService : IPaginationService
{
    private readonly IUriService _uriService;
    private readonly IMapper _mapper;
    public PaginationService(IUriService uriService, IMapper mapper)
    {
        _uriService = uriService;
        _mapper = mapper;
    }

    public PagedResponse<PagedListViewModel<T>> CreatePagedResponse<T>(PagedListViewModel<T> pagedData)
    {
        var nextPage = pagedData.HasNext && pagedData.CurrentPage < pagedData.TotalPages
            ? _uriService.GetPageUri(pagedData.CurrentPage + 1, pagedData.PageSize)
            : null;
        var previousPage = pagedData.HasPrevious && pagedData.CurrentPage <= pagedData.TotalPages
            ? _uriService.GetPageUri(pagedData.CurrentPage - 1, pagedData.PageSize)
            : null;
        var firstPage = _uriService.GetPageUri(1, pagedData.PageSize);
        var lastPage = _uriService.GetPageUri(pagedData.TotalPages, pagedData.PageSize);

        var links = new List<Link>()
        {
            new Link(nextPage, "next"),
            new Link(previousPage, "prev"),
            new Link(firstPage, "first"),
            new Link(lastPage, "last")
        };

        links.RemoveAll(x => x.Href == null);

        var response = new PagedResponse<PagedListViewModel<T>>(pagedData, pagedData.CurrentPage, pagedData.PageSize, pagedData.TotalPages, pagedData.TotalCount)
        {
            Links = links
        };

        return response;
    }

    public PagedListViewModel<TDestination> ToPagedListViewModelAsync<TSource, TDestination>(PagedList<TSource> source)
    {
        var mappedSource = _mapper.Map<PagedListViewModel<TDestination>>(source);
        mappedSource.CurrentPage = source.CurrentPage;
        mappedSource.TotalPages = source.TotalPages;
        mappedSource.PageSize = source.PageSize;
        mappedSource.TotalCount = source.TotalCount;
        mappedSource.HasPrevious = source.HasPrevious;
        mappedSource.HasNext = source.HasNext;

        return mappedSource;
    }
}
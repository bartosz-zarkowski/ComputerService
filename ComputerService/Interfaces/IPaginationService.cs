using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IPaginationService
{
    public PagedResponse<PagedListViewModel<T>> CreatePagedResponse<T>(PagedListViewModel<T> pagedData);

    PagedListViewModel<TDestination> ToPagedListViewModelAsync<TSource, TDestination>(PagedList<TSource> source);
}
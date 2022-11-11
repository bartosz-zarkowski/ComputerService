using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IPaginationService
{
    public PagedResponse<PagedListViewModel<T>> CreatePagedResponse<T>(PagedListViewModel<T> pagedData, ParametersModel? parameters, Enum? sortOrder);

    PagedListViewModel<TDestination> ToPagedListViewModelAsync<TSource, TDestination>(PagedList<TSource> source);
}
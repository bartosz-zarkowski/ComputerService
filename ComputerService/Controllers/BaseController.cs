using AutoMapper;
using ComputerService.Exceptions;
using ComputerService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

public class BaseController<T> : ControllerBase
{
    protected IPaginationService PaginationService { get; set; }
    protected IMapper Mapper { get; set; }
    protected ILogger<BaseController<T>> Logger { get; set; }

    public BaseController(IPaginationService paginationService, IMapper mapper, ILogger<BaseController<T>> logger)
    {
        PaginationService = paginationService;
        Mapper = mapper;
        Logger = logger;
    }

    protected void CheckIfEntityExists(T entity, string message)
    {
        if (entity == null)
        {
            throw new NotFoundException(message);
        }
    }
}
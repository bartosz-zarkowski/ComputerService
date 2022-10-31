using AutoMapper;
using ComputerService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

public class BaseController<T> : ControllerBase
{
    protected IMapper Mapper { get; set; }
    protected ILogger<BaseController<T>> Logger { get; set; }

    public BaseController(IMapper mapper, ILogger<BaseController<T>> logger)
    {
        Mapper = mapper;
        Logger = logger;
    }

    protected void CheckIfEntityExists(T entity)
    {
        if (entity == null)
        {
            throw new NotFoundException();
        }
    }
}

using AutoMapper;
using ComputerService.Data;
using ComputerService.Exceptions;
using ComputerService.Interfaces;
using ComputerService.Repository;
using FluentValidation;

namespace ComputerService.Services;

public class BaseEntityService<T> : BaseRepository<T>, IBaseEntityService<T> where T : class
{
    private ComputerServiceContext Context { get; set; }
    private IValidator<T> Validator { get; set; }
    private IMapper Mapper { get; set; }

    public BaseEntityService(ComputerServiceContext context, IValidator<T> validator, IMapper mapper) : base(context)
    {
        Context = context;
        Validator = validator;
        Mapper = mapper;
    }

    public async Task ValidateEntityAsync(T entity)
    {
        var validationResult = await Validator.ValidateAsync(entity);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.Errors);
        }
    }
}

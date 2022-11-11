namespace ComputerService.Interfaces;
public interface IBaseEntityService<T>
{
    Task ValidateEntityAsync(T entity);
}

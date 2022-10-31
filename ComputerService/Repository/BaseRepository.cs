using ComputerService.Data;
using System.Linq.Expressions;

namespace ComputerService.Repository;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private ComputerServiceContext Context { get; set; }

    protected BaseRepository(ComputerServiceContext context)
    {
        Context = context;
    }

    public IQueryable<T> FindAll()
    {
        return Context.Set<T>().AsQueryable();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>()
            .Where(expression)
            .AsQueryable();
    }

    public async Task CreateAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}
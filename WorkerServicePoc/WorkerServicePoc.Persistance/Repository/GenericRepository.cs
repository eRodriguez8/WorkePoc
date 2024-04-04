using Microsoft.EntityFrameworkCore;

namespace WorkerServicePoc.Persistance.Repository;

internal class GenericRepository<T> where T : class
{
    protected readonly WorkerDbContext DbContext;

    protected GenericRepository(WorkerDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.Set<T>().ExecuteDeleteAsync(cancellationToken);
    }
}
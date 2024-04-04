using Microsoft.EntityFrameworkCore;
using WorkerServicePoc.Domain.Abstractions;
using WorkerServicePoc.Domain.Outbox;

namespace WorkerServicePoc.Persistance;

public sealed class WorkerDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkerDbContext).Assembly);
    }
}

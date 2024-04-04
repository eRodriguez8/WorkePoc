using Microsoft.EntityFrameworkCore;
using WorkerServicePoc.Domain.Outbox;
using WorkerServicePoc.Persistance.Repository;

namespace WorkerServicePoc.Persistance.Outbox;

internal sealed class OutboxRepository(WorkerDbContext dbContext) : GenericRepository<OutboxMessage>(dbContext), IOutboxRepository
{
    public async Task<IEnumerable<OutboxMessage>> GetUnprocessOutboxMessagesAsync(CancellationToken cancellationToken)
        => await DbContext.OutboxMessages.Where(item => item.ProcessedOnUtc == null).ToListAsync(cancellationToken);

    public async Task<IEnumerable<OutboxMessage>> GetAllAsync(CancellationToken cancellationToken)
        => await DbContext.OutboxMessages
        .AsNoTracking()
        .ToListAsync(cancellationToken);
}

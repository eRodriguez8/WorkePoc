namespace WorkerServicePoc.Domain.Outbox;

public interface IOutboxRepository
{
    void Add(OutboxMessage outboxMessage);
    Task ClearAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<OutboxMessage>> GetUnprocessOutboxMessagesAsync(CancellationToken cancellationToken = default);
    Task<OutboxMessage> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<OutboxMessage>> GetAllAsync(CancellationToken cancellationToken = default);
}

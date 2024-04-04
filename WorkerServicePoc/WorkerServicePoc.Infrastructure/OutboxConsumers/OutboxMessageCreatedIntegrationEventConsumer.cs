using MassTransit;
using Microsoft.Extensions.Logging;
using WorkerServicePoc.Application.PublishOutbox;
using WorkerServicePoc.Domain.Abstractions;
using WorkerServicePoc.Domain.Outbox;

namespace WorkerServicePoc.Infrastructure.OutboxConsumers;

public sealed class OutboxMessageCreatedIntegrationEventConsumer(
    IUnitOfWork unitOfWork,
    IOutboxRepository repository,
    ILogger<OutboxMessageCreatedIntegrationEventConsumer> logger) : IConsumer<OutboxMessageCreatedIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOutboxRepository _repository = repository;
    private readonly ILogger<OutboxMessageCreatedIntegrationEventConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<OutboxMessageCreatedIntegrationEvent> context)
    {
        OutboxMessage outboxMessage = await _repository.GetByIdAsync(context.Message.OutboxId);
        try
        {
            // Do some work
            await Task.Delay(1000);
        }
        catch (Exception ex)
        {
            outboxMessage.SetError(ex.Message);
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Message consume {Message}", outboxMessage);
    }
}

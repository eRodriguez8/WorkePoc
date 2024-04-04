using Microsoft.Extensions.Logging;
using WorkerServicePoc.Application.Abstractions.Commands;
using WorkerServicePoc.Application.Abstractions.Events;
using WorkerServicePoc.Application.PublishOutbox.Extensions;
using WorkerServicePoc.Domain.Abstractions;
using WorkerServicePoc.Domain.Outbox;

namespace WorkerServicePoc.Application.PublishOutbox;

internal sealed class PubishOutboxMessagesHandler(
    IEventBus eventBus,
    IUnitOfWork unitOfWork,
    IOutboxRepository repository,
    ILogger<PubishOutboxMessagesHandler> logger) : ICommandHandler
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOutboxRepository _repository = repository;
    private readonly ILogger<PubishOutboxMessagesHandler> _logger = logger;

    public async Task<Result> HandleAsync(CancellationToken cancellationToken)
    {
        try
		{
            _logger.LogInformation("Starting {PubishOutboxMessagesHandler} at {DateTimeHandler}.", nameof(PubishOutboxMessagesHandler), DateTime.Now);

            IEnumerable<OutboxMessage> records = await _repository.GetUnprocessOutboxMessagesAsync(cancellationToken);

            if(!records.Any())
            {
                _logger.LogInformation("Finish {PubishOutboxMessagesHandler} at {DateTimeHandler}. No items to process.", nameof(PubishOutboxMessagesHandler), DateTime.Now);

                return Result.Success();
            }

            foreach (OutboxMessage item in records)
            {
                if (item.HasProcessedOnUtc())
                {
                    continue;
                }

                _logger.LogInformation("Publishing outbox message {OutboxMessageId}. Content: {OutboxMessageContent}", item.Id, item.Content);

                await _eventBus.PublishAsync(item.ToIntegrationEvent(), cancellationToken);

                item.AdvanceState();

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Publish outbox message {OutboxMessageId} success.", item.Id);
            }

            _logger.LogInformation("Finish {PubishOutboxMessagesHandler} at {DateTimeHandler}.", nameof(PubishOutboxMessagesHandler), DateTime.Now);

            return Result.Success();
        }
        catch (Exception ex)
		{
			return Result.Failure(new Error("PubishOutboxMessagesHandler.Error", $"Failed to publish records. Message {ex.Message}"));
		}
    }
}

using WorkerServicePoc.Api.Abstractions;
using WorkerServicePoc.Domain.Abstractions;
using WorkerServicePoc.Domain.Outbox;

namespace WorkerServicePoc.Api.Endpoints.Outbox;

public sealed class CreateOutboxEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "outbox",
            async (CreateOutboxRequest request, IUnitOfWork unitOfWork, IOutboxRepository repository, CancellationToken cancellationToken) =>
            {
                repository.Add(OutboxMessage.Create(request.Type, request.Content));

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Results.Created();
            });

        app.MapGet(
            "outbox",
            async (IOutboxRepository repository, CancellationToken cancellationToken) =>
            {
                IEnumerable<OutboxMessage> records = await repository.GetAllAsync(cancellationToken);

                return Results.Ok(records);
            });

        app.MapDelete(
            "outbox",
            async (IUnitOfWork unitOfWork, IOutboxRepository repository, CancellationToken cancellationToken) =>
            {
                await repository.ClearAsync(cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Results.NoContent();
            });
    }
}

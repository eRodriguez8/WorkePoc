using WorkerServicePoc.Domain.Abstractions;

namespace WorkerServicePoc.Application.Abstractions.Commands;

public interface ICommandHandler
{
    Task<Result> HandleAsync(CancellationToken cancellationToken);
}
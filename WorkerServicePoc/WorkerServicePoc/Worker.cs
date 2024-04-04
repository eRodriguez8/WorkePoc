using Microsoft.Extensions.Options;
using WorkerServicePoc.Application.Abstractions.Commands;
using WorkerServicePoc.Domain.Abstractions;
using WorkerServicePoc.WorkerSettings;

namespace WorkerServicePoc;

public class Worker(
    ILogger<Worker> logger,
    IOptions<WorkerPeriodSettings> options,
    IServiceScopeFactory serviceScopeFactory
    ) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly WorkerPeriodSettings _options = options.Value;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(_options.PeriodInSeconds));

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            ICommandHandler commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler>();

            Result result = await commandHandler.HandleAsync(stoppingToken);
            if (result.IsFailure)
            {
                _logger.LogError("Error executing handler. Message: {ErrorMessage}", result.Error);
            }
        }
    }
}

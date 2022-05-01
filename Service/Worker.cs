using RequestService.Common.Configuration;
using RequestService.Common.HttpClients;
using RequestService.Services;

namespace RequestService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    /// <summary>
    /// Entry point of the worker service.
    /// </summary>
    /// <param name="stoppingToken">Notification that execution should be stopped.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var intensityService = scope.ServiceProvider.GetRequiredService<IIntensityService>();
                var requestsConfiguration = scope.ServiceProvider.GetRequiredService<RequestsConfiguration>();
                var requestsHttpClient = scope.ServiceProvider.GetRequiredService<IRequestsHttpClient>();
                int delayInMillis = intensityService.DelayInMillis;

                Parallel.For(0, requestsConfiguration.RoutesCount, route =>
                {
                    _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                    _logger.LogInformation("Current delay: {Delay}", delayInMillis);
                });

                await Task.Delay(delayInMillis, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Execution failed.");
            }
        }
    }
}
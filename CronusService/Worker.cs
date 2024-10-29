using Elders.Cronus.Discoveries;
using Elders.Cronus;
using System.Diagnostics;
using CronusService.Commands;
using CronusService.Identifications;
using Elders.Cronus.Projections;
using CronusService;
using Elders.Cronus.MessageProcessing;
using Elders.Cronus.Api;

public class Worker : BackgroundService
{
    private IHost cronusDashboard;
    private readonly ILogger<Worker> _logger;
    private readonly ICronusHost cronusHost;
    private readonly IPublisher<ICommand> _publisher;
    private readonly IProjectionReader _projectionReader;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICronusApiAccessor cronusApi;

    public Worker(ILogger<Worker> logger, ICronusHost cronusHost, IPublisher<ICommand> publisher, IServiceProvider serviceProvider, ICronusApiAccessor cronusApi)
    {
        _logger = logger;
        this.cronusHost = cronusHost;
        _publisher = publisher;
        _serviceProvider = serviceProvider;
        this.cronusApi = cronusApi;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting service...");

        await cronusHost.StartAsync();

        cronusDashboard = CronusApi.GetHost();
        cronusApi.Provider = cronusDashboard.Services;
        await cronusDashboard.StartAsync().ConfigureAwait(false);

        _logger.LogInformation("Service started!");

    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping service...");

        await cronusHost.StopAsync();
        await cronusDashboard.StopAsync(TimeSpan.FromSeconds(1));

        _logger.LogInformation("Service stopped");
    }

    public override void Dispose()
    {
        StopAsync(CancellationToken.None).GetAwaiter().GetResult();
    }
}
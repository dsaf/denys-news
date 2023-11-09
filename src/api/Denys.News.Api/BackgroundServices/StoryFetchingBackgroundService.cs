using System;
using System.Threading;
using System.Threading.Tasks;
using Denys.News.Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Denys.News.Api.BackgroundServices;

public sealed class StoryFetchingBackgroundService : BackgroundService
{
    private readonly ILogger<StoryFetchingBackgroundService> _logger;
    private readonly IStoryFetchingService _storyFetchingService;
    private readonly TimeSpan _interval;

    public StoryFetchingBackgroundService(ILogger<StoryFetchingBackgroundService> logger, IStoryFetchingService storyFetchingService)
    {
        _logger = logger;
        _storyFetchingService = storyFetchingService;

        _interval = TimeSpan.FromSeconds(60);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_interval);

        do
        {
            await FetchAsync();
        } while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task FetchAsync()
    {
        _logger.LogTrace($"{nameof(FetchAsync)} {DateTime.Now}");

        try
        {
            await _storyFetchingService.FetchAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
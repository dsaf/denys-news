using System.Collections.Generic;
using Denys.News.Core.Dtos;
using Denys.News.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Denys.News.Api.Controllers;

[ApiController, Route("[controller]")]
public sealed class StoriesController : ControllerBase
{
    private readonly ILogger<StoriesController> _logger;
    private readonly IStoryQueryService _storyQueryService;

    public StoriesController(ILogger<StoriesController> logger, IStoryQueryService storyQueryService)
    {
        _logger = logger;
        _storyQueryService = storyQueryService;
    }

    [HttpGet]
    public IEnumerable<StoryHeaderDto> Get([FromQuery(Name = "n")] int number)
    {
        _logger.LogTrace($"{nameof(Get)} {nameof(number)}={{number}}", number);

        return _storyQueryService.GetBest(number);
    }
}
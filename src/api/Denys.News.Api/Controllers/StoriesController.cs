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
    private readonly IStoryService _storyService;

    public StoriesController(ILogger<StoriesController> logger, IStoryService storyService)
    {
        _logger = logger;
        _storyService = storyService;
    }

    [HttpGet]
    public IEnumerable<StoryHeaderDto> Get([FromQuery(Name = "n")] int number)
    {
        _logger.LogTrace($"{nameof(Get)} {nameof(number)}={{number}}", number);

        return _storyService.GetBest(number);
    }
}
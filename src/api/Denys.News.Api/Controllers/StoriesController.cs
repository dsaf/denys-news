using Denys.News.Core.Dtos;
using Denys.News.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Denys.News.Api.Controllers;

[ApiController, Route("api/v1/[controller]"), Produces("application/json")]
public sealed class StoriesController : ControllerBase
{
    private readonly ILogger<StoriesController> _logger;
    private readonly IStoryQueryService _storyQueryService;

    public StoriesController(ILogger<StoriesController> logger, IStoryQueryService storyQueryService)
    {
        _logger = logger;
        _storyQueryService = storyQueryService;
    }

    /// <summary>Retrieve the details of the best n stories from the Hacker News, as determined by their score</summary>
    /// <param name="number">Number of stories to retrieve</param>
    /// <returns>The best n stories</returns>
    /// <response code="200">Returns the stories</response>
    /// <response code="400">If the number is not a positive integer</response>
    /// <response code="500">In case of unexpected internal errors</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<StoryHeaderDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromQuery(Name = "n")] int number)
    {
        _logger.LogTrace($"{nameof(Get)} {nameof(number)}={{number}}", number);

        return number > 0
            ? Ok(_storyQueryService.GetBest(number))
            : BadRequest("n is not a positive integer");
    }
}
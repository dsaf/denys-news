using Microsoft.AspNetCore.Mvc;

namespace Denys.News.Api.Controllers
{
    [ApiController, Route("[controller]")]
    public sealed class StoriesController : ControllerBase
    {
        private readonly ILogger<StoriesController> _logger;

        public StoriesController(ILogger<StoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get([FromQuery(Name = "n")] int number)
        {
            return Enumerable.Range(1, number).Select(x => $"story {x}");
        }
    }
}
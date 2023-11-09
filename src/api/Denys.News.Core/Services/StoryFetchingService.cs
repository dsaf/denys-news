using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Denys.News.Core.Clients;
using Denys.News.Core.Configuration;
using Denys.News.Core.Dtos;
using Denys.News.Core.Repositories;
using Microsoft.Extensions.Options;

namespace Denys.News.Core.Services;

public sealed class StoryFetchingService : IStoryFetchingService
{
    private readonly IStoryWriteRepository _repository;
    private readonly IHackerNewsClient _hackerNewsClient;
    private readonly int _parallelRequests;

    public StoryFetchingService(IOptions<StoryFetchingOptions> options, IStoryWriteRepository repository, IHackerNewsClient hackerNewsClient)
    {
        _repository = repository;
        _hackerNewsClient = hackerNewsClient;

        _parallelRequests = options.Value.ParallelRequests ?? throw new ArgumentOutOfRangeException();
    }

    public async ValueTask FetchAsync()
    {
        var ids = await _hackerNewsClient.GetBestStoryIds();

        var stories = new List<StoryHeaderDto>();

        foreach (var idChunk in ids.Chunk(_parallelRequests))
        {
            var tasks = idChunk.Select(_hackerNewsClient.GetStory);

            var hackerNewStoryDtos = await Task.WhenAll(tasks);

            var storyHeaderDtos = hackerNewStoryDtos.Select(Map);

            stories.AddRange(storyHeaderDtos);
        }

        _repository.SetBestStories(stories.OrderByDescending(x => x.Score).ToArray());
    }

    private static StoryHeaderDto Map(HackerNewsStoryDto hackerNewStoryDto)
    {
        return new StoryHeaderDto
        {
            Title = hackerNewStoryDto.Title,
            Uri = hackerNewStoryDto.Url,
            PostedBy = hackerNewStoryDto.By,
            Time = Map(hackerNewStoryDto.Time),
            Score = hackerNewStoryDto.Score,
            CommentCount = hackerNewStoryDto.Descendants
        };
    }

    private static string Map(int unixTime)
    {
        return DateTimeOffset
            .FromUnixTimeSeconds(unixTime)
            .ToString(@"yyyy-MM-ddTHH\:mm\:sszzz", CultureInfo.InvariantCulture);
    }
}
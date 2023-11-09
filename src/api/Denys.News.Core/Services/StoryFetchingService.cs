using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Denys.News.Core.Clients;
using Denys.News.Core.Dtos;
using Denys.News.Core.Repositories;

namespace Denys.News.Core.Services;

public sealed class StoryFetchingService : IStoryFetchingService
{
    private readonly IStoryWriteRepository _repository;
    private readonly IHackerNewsClient _hackerNewsClient;

    public StoryFetchingService(IStoryWriteRepository repository, IHackerNewsClient hackerNewsClient)
    {
        _repository = repository;
        _hackerNewsClient = hackerNewsClient;
    }

    public async ValueTask FetchAsync()
    {
        var ids = await _hackerNewsClient.GetBestStoryIds();

        var stories = new List<StoryHeaderDto>();

        foreach (var id in ids)
        {
            var hackerNewStoryDto = await _hackerNewsClient.GetStory(id);

            var storyHeaderDto = Map(hackerNewStoryDto);

            stories.Add(storyHeaderDto);
        }

        _repository.SetBestStories(stories);
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
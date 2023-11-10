using System.Globalization;
using System;

namespace Denys.News.Core.Dtos;

public static class DtoMapping
{
    public static StoryHeaderDto MapToStoryHeader(HackerNewsStoryDto hackerNewStoryDto)
    {
        return new StoryHeaderDto
        {
            Title = hackerNewStoryDto.Title,
            Uri = hackerNewStoryDto.Url,
            PostedBy = hackerNewStoryDto.By,
            Time = hackerNewStoryDto.Time != default ? MapToIso8601(hackerNewStoryDto.Time) : null,
            Score = hackerNewStoryDto.Score,
            CommentCount = hackerNewStoryDto.Descendants
        };
    }

    public static string MapToIso8601(int unixTime)
    {
        return DateTimeOffset
            .FromUnixTimeSeconds(unixTime)
            .ToString(@"yyyy-MM-ddTHH\:mm\:sszzz", CultureInfo.InvariantCulture);
    }
}
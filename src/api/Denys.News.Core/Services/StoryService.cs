using System.Collections.Generic;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Services;

public sealed class StoryService : IStoryService
{
    public IReadOnlyCollection<StoryHeaderDto> GetBest(int number)
    {
        return new[]
        {
            new StoryHeaderDto
            {
                Title = "A uBlock Origin update was rejected from the Chrome Web Store",
                Uri = "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
                PostedBy = "ismaildonmez",
                Time = "2019-10-12T13:43:01+00:00",
                Score = 1716,
                CommentCount = 572
            }
        };
    }
}
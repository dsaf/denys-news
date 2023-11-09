using System.Collections.Generic;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Repositories;

public interface IStoryReadRepository
{
    public IReadOnlyCollection<StoryHeaderDto> GetBestStories(int number);
}
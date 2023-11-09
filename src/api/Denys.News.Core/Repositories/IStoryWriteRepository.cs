using System.Collections.Generic;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Repositories;

public interface IStoryWriteRepository
{
    public void SetBestStories(IReadOnlyCollection<StoryHeaderDto> dtos);
}
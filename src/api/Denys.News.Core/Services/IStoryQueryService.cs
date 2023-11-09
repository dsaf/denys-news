using System.Collections.Generic;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Services;

public interface IStoryQueryService
{
    IReadOnlyCollection<StoryHeaderDto> GetBest(int number);
}
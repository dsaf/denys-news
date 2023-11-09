using System.Collections.Generic;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Services;

public interface IStoryService
{
    IReadOnlyCollection<StoryHeaderDto> GetBest(int number);
}
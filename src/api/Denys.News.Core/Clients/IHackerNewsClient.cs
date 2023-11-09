using System.Collections.Generic;
using System.Threading.Tasks;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Clients;

public interface IHackerNewsClient
{
    Task<IReadOnlyCollection<int>> GetBestStoryIds();
    Task<HackerNewsStoryDto> GetStory(int id);
}
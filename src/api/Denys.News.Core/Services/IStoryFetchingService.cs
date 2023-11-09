using System.Threading.Tasks;

namespace Denys.News.Core.Services;

public interface IStoryFetchingService
{
    ValueTask FetchAsync();
}
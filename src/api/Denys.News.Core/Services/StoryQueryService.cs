using System.Collections.Generic;
using Denys.News.Core.Dtos;
using Denys.News.Core.Repositories;

namespace Denys.News.Core.Services;

public sealed class StoryQueryService : IStoryQueryService
{
    private readonly IStoryReadRepository _repository;

    public StoryQueryService(IStoryReadRepository repository)
        => _repository = repository;

    public IReadOnlyCollection<StoryHeaderDto> GetBest(int number)
    {
        return _repository.GetBestStories(number);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Repositories;

public sealed class InMemoryStoryRepository : IStoryReadRepository, IStoryWriteRepository
{
    private IReadOnlyCollection<StoryHeaderDto> _dtos = Array.Empty<StoryHeaderDto>();

    public void SetBestStories(IReadOnlyCollection<StoryHeaderDto> dtos) => _dtos = dtos;

    public IReadOnlyCollection<StoryHeaderDto> GetBestStories(int number) => _dtos.Take(number).ToArray();
}
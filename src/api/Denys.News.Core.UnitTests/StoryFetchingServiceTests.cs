using System.Collections.Generic;
using System.Threading.Tasks;
using Denys.News.Core.Clients;
using Denys.News.Core.Configuration;
using Denys.News.Core.Dtos;
using Denys.News.Core.Repositories;
using Denys.News.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Denys.News.Core.UnitTests;

public sealed class StoryFetchingServiceTests
{
    [Test]
    public async ValueTask FetchAsync_Works_AsExpected()
    {
        var optionsMock = Options.Create(new StoryFetchingOptions { ParallelRequests = 2 });
        var repositoryMock = new Mock<IStoryWriteRepository>();
        var clientMock = new Mock<IHackerNewsClient>();

        IReadOnlyCollection<StoryHeaderDto> actual = null;

        repositoryMock.Setup(x => x.SetBestStories(It.IsAny<IReadOnlyCollection<StoryHeaderDto>>()))
            .Callback((IReadOnlyCollection<StoryHeaderDto> dtos) => actual = dtos);

        clientMock.Setup(x => x.GetBestStoryIds()).ReturnsAsync(new[] { 1, 2, 3 });
        clientMock.Setup(x => x.GetStory(It.IsAny<int>())).ReturnsAsync((int id) => new HackerNewsStoryDto { Title = $"Story {id}", Score = id*100});

        var service = new StoryFetchingService(optionsMock, repositoryMock.Object, clientMock.Object);

        await service.FetchAsync();

        var expected = new[]
        {
            new StoryHeaderDto { Title = "Story 3", Score = 300 },
            new StoryHeaderDto { Title = "Story 2", Score = 200 },
            new StoryHeaderDto { Title = "Story 1", Score = 100 }
        };

        actual.Should().BeEquivalentTo(expected, x => x.WithStrictOrdering());

        clientMock.Verify(x => x.GetBestStoryIds(), Times.Once);
        clientMock.Verify(x => x.GetStory(It.IsAny<int>()), Times.Exactly(3));
        clientMock.VerifyNoOtherCalls();

        repositoryMock.Verify(x => x.SetBestStories(It.IsAny<StoryHeaderDto[]>()), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }
}
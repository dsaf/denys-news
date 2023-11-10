using Denys.News.Core.Dtos;
using FluentAssertions;
using NUnit.Framework;

namespace Denys.News.Core.UnitTests;

public sealed class DtoMappingTests
{
    [Test]
    public void MapToIso8601_Works_AsExpected()
    {
        var actual = DtoMapping.MapToIso8601(1570887781);

        actual.Should().BeEquivalentTo("2019-10-12T13:43:01+00:00");
    }

    [Test]
    public void MapToStoryHeader_Works_AsExpected()
    {
        var input = new HackerNewsStoryDto
        {
            By = "utest",
            Descendants = 100,
            Score = 200,
            Time = 1676043045,
            Title = "Story for a unit test",
            Url = "http://localhost:80/unit/test"
        };

        var expected = new StoryHeaderDto
        {
            Title = "Story for a unit test",
            Uri = "http://localhost:80/unit/test",
            PostedBy = "utest",
            Time = "2023-02-10T15:30:45+00:00",
            Score = 200,
            CommentCount = 100
        };

        var actual = DtoMapping.MapToStoryHeader(input);

        actual.Should().BeEquivalentTo(expected);
    }
}
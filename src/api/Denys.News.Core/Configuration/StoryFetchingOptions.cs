namespace Denys.News.Core.Configuration;

public sealed class StoryFetchingOptions
{
    public const string Key = "StoryFetching";

    public int? ParallelRequests { get; set; }
}
namespace Denys.News.Core.Configuration;

public sealed class StoryRefreshingOptions
{
    public const string Key = "StoryRefreshing";

    public int? IntervalMs { get; set; }
}
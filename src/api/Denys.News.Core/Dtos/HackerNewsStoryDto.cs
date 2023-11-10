namespace Denys.News.Core.Dtos;

public sealed class HackerNewsStoryDto
{
    public string By { get; set; }
    public int Descendants { get; set; }
    public int Score { get; set; }
    public int Time { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
}
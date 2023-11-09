using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Denys.News.Core.Dtos;

namespace Denys.News.Core.Clients;

public sealed class HackerNewsClient : IHackerNewsClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseUri;

    public HackerNewsClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _baseUri = "https://hacker-news.firebaseio.com/v0";
    }

    public async Task<IReadOnlyCollection<int>> GetBestStoryIds()
    {
        using var client = _httpClientFactory.CreateClient();

        var result = await client.GetFromJsonAsync<int[]>(new Uri(new Uri(_baseUri), "beststories.json"));

        return result;
    }

    public async Task<HackerNewsStoryDto> GetStory(int id)
    {
        using var client = _httpClientFactory.CreateClient();

        var result = await client.GetFromJsonAsync<HackerNewsStoryDto>(new Uri(new Uri(_baseUri), $"item/{id}.json"));

        return result;
    }
}
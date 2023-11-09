using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Denys.News.Core.Configuration;
using Denys.News.Core.Dtos;
using Microsoft.Extensions.Options;

namespace Denys.News.Core.Clients;

public sealed class HackerNewsClient : IHackerNewsClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseUri;

    public HackerNewsClient(IHttpClientFactory httpClientFactory, IOptions<HackerNewsApiOptions> options)
    {
        _httpClientFactory = httpClientFactory;

        _baseUri = options.Value.BaseUri ?? throw new ArgumentOutOfRangeException();
    }

    public async Task<IReadOnlyCollection<int>> GetBestStoryIds()
    {
        using var client = _httpClientFactory.CreateClient();

        var uri = new Uri(new Uri(_baseUri), "v0/beststories.json");
        var result = await client.GetFromJsonAsync<int[]>(uri);

        return result;
    }

    public async Task<HackerNewsStoryDto> GetStory(int id)
    {
        using var client = _httpClientFactory.CreateClient();

        var uri = new Uri(new Uri(_baseUri), $"v0/item/{id}.json");
        var result = await client.GetFromJsonAsync<HackerNewsStoryDto>(uri);

        return result;
    }
}
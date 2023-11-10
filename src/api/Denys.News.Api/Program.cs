using System;
using System.IO;
using System.Reflection;
using Denys.News.Api.BackgroundServices;
using Denys.News.Core.Clients;
using Denys.News.Core.Configuration;
using Denys.News.Core.Repositories;
using Denys.News.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HackerNewsApiOptions>(builder.Configuration.GetSection(HackerNewsApiOptions.Key));
builder.Services.Configure<StoryFetchingOptions>(builder.Configuration.GetSection(StoryFetchingOptions.Key));
builder.Services.Configure<StoryRefreshingOptions>(builder.Configuration.GetSection(StoryRefreshingOptions.Key));

builder.Services.AddSingleton<InMemoryStoryRepository>();
builder.Services.AddSingleton<IStoryReadRepository>(x => x.GetService<InMemoryStoryRepository>() ?? throw new InvalidOperationException());
builder.Services.AddSingleton<IStoryWriteRepository>(x => x.GetService<InMemoryStoryRepository>() ?? throw new InvalidOperationException());

builder.Services.AddSingleton<IHackerNewsClient, HackerNewsClient>();
builder.Services.AddSingleton<IStoryQueryService, StoryQueryService>();
builder.Services.AddSingleton<IStoryFetchingService, StoryFetchingService>();

builder.Services.AddHostedService<StoryRefreshingBackgroundService>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Denys News API", Description = "API to retrieve stories from the Hacker News" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
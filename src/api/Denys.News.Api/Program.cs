using System;
using Denys.News.Api.BackgroundServices;
using Denys.News.Core.Clients;
using Denys.News.Core.Repositories;
using Denys.News.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<InMemoryStoryRepository>();
builder.Services.AddSingleton<IStoryReadRepository>(
    x => x.GetService<InMemoryStoryRepository>() ?? throw new InvalidOperationException());
builder.Services.AddSingleton<IStoryWriteRepository>(
    x => x.GetService<InMemoryStoryRepository>() ?? throw new InvalidOperationException());

builder.Services.AddSingleton<IHackerNewsClient, HackerNewsClient>();
builder.Services.AddSingleton<IStoryQueryService, StoryQueryService>();
builder.Services.AddSingleton<IStoryFetchingService, StoryFetchingService>();

builder.Services.AddHostedService<StoryFetchingBackgroundService>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.UsageDetector;
using NGame.Assets.UsageDetector.AssetOverviews;
using NGame.Assets.UsageDetector.AssetUsages;
using NGame.Assets.UsageDetector.Commands;
using NGame.Assets.UsageDetector.FileWriters;
using NGame.Tooling.Setup;


var builder = Host.CreateApplicationBuilder(args);


builder.AddNGameCommon();

builder.Services.Configure<CommandArguments>(builder.Configuration);
builder.Services.AddTransient<ICommandValidator, CommandValidator>();

builder.Services.AddTransient<ICommandRunner, CommandRunner>();

builder.Services.AddTransient<IAssetOverviewCreator, AssetOverviewCreator>();
builder.Services.AddTransient<IAssetListReader, AssetListReader>();

builder.Services.AddTransient<IAssetUsageFinder, AssetUsageFinder>();
builder.Services.AddTransient<IJsonNodeAssetIdFinder, JsonNodeAssetIdFinder>();

builder.Services.AddTransient<IUsedAssetsFileWriter, UsedAssetsFileWriter>();




var host = builder.Build();


var commandRunner = host.Services.GetRequiredService<ICommandRunner>();
commandRunner.Run();

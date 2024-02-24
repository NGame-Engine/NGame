using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Setup;
using NGame.Assets.Packer;
using NGame.Assets.Packer.AssetOverviews;
using NGame.Assets.Packer.AssetUsages;
using NGame.Assets.Packer.Commands;
using NGame.Assets.Packer.FileWriters;


var builder = Host.CreateApplicationBuilder(args);


builder.AddNGameAssetsCommon();

builder.Services.Configure<CommandArguments>(builder.Configuration);
builder.Services.AddTransient<ICommandValidator, CommandValidator>();

builder.Services.AddTransient<ICommandRunner, CommandRunner>();

builder.Services.AddTransient<IAssetOverviewCreator, AssetOverviewCreator>();
builder.Services.AddTransient<IAssetListReader, AssetListReader>();

builder.Services.AddTransient<IAssetUsageFinder, AssetUsageFinder>();
builder.Services.AddTransient<IJsonNodeAssetIdFinder, JsonNodeAssetIdFinder>();

builder.Services.AddTransient<IAssetPackFileWriter, AssetPackFileWriter>();
builder.Services.AddTransient<IAssetPackFactory, AssetPackFactory>();
builder.Services.AddTransient<ITableOfContentsWriter, TableOfContentsWriter>();


var host = builder.Build();


var commandRunner = host.Services.GetRequiredService<ICommandRunner>();
commandRunner.Run();

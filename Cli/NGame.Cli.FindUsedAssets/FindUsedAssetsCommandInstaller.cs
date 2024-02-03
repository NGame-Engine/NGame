using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Cli.Abstractions;
using NGame.Cli.FindUsedAssets.AssetOverviews;
using NGame.Cli.FindUsedAssets.AssetUsages;
using NGame.Cli.FindUsedAssets.Commands;
using NGame.Cli.FindUsedAssets.FileWriters;

namespace NGame.Cli.FindUsedAssets;



public static class FindUsedAssetsCommandInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder InstallFindUsedAssetsCommand(this IHostApplicationBuilder builder)
	{
		builder.Services.Configure<CommandArguments>(builder.Configuration);
		builder.Services.AddTransient<ICommandValidator, CommandValidator>();

		builder.Services.AddTransient<ICommandRunner, PackFindUsedAssetsCommandRunner>();

		builder.Services.AddTransient<IAssetOverviewCreator, AssetOverviewCreator>();
		builder.Services.AddTransient<IAssetListReader, AssetListReader>();

		builder.Services.AddTransient<IAssetUsageFinder, AssetUsageFinder>();
		builder.Services.AddTransient<IJsonNodeAssetIdFinder, JsonNodeAssetIdFinder>();

		builder.Services.AddTransient<IUsedAssetsFileWriter, UsedAssetsFileWriter>();
		builder.Services.AddTransient<IAssetFileSpecificationFactory, AssetFileSpecificationFactory>();
		builder.Services.AddTransient<IAssetPackFactory, AssetPackFactory>();
		builder.Services.AddTransient<ITableOfContentsGenerator, TableOfContentsGenerator>();
		builder.Services.AddTransient<ITableOfContentsWriter, TableOfContentsWriter>();

		return builder;
	}
}

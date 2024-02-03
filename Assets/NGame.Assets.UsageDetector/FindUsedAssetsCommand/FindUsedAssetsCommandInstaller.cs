using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Cli.Abstractions;
using NGame.Cli.FindUsedAssetsCommand.AssetOverviews;
using NGame.Cli.FindUsedAssetsCommand.AssetUsages;
using NGame.Cli.FindUsedAssetsCommand.Commands;
using NGame.Cli.FindUsedAssetsCommand.FileWriters;

namespace NGame.Cli.FindUsedAssetsCommand;



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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Cli.Abstractions;
using NGame.Cli.PackAssets.AssetFileReaders;
using NGame.Cli.PackAssets.CommandValidators;
using NGame.Cli.PackAssets.Specifications;
using NGame.Cli.PackAssets.Writers;

namespace NGame.Cli.PackAssets;



public static class PackAssetsCommandInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder InstallPackAssetsCommand(this IHostApplicationBuilder builder)
	{
		builder.Services.Configure<CommandArguments>(builder.Configuration);


		builder.Services.AddTransient<ICommandRunner, PackAssetsCommandRunner>();


		builder.Services.AddTransient<ICommandValidator, CommandValidator>();


		builder.Services.AddTransient<IAssetListLineParser, AssetListLineParser>();


		builder.Services.AddTransient<ISpecificationCreator, SpecificationCreator>();


		builder.Services.AddTransient<IAssetsWriter, AssetsWriter>();

		builder.Services.AddTransient<IAssetPackSpecificationWriter, AssetPackSpecificationWriter>();

		builder.Services.AddTransient<IBasicAssetReader, BasicAssetReader>();

		builder.Services.AddTransient<ITableOfContentsGenerator, TableOfContentsGenerator>();
		builder.Services.AddTransient<ITableOfContentsWriter, TableOfContentsWriter>();

		return builder;
	}
}

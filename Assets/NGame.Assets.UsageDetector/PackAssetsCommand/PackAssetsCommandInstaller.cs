using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Cli.Abstractions;
using NGame.Cli.PackAssetsCommand.AssetFileReaders;
using NGame.Cli.PackAssetsCommand.CommandValidators;
using NGame.Cli.PackAssetsCommand.Specifications;
using NGame.Cli.PackAssetsCommand.Writers;

namespace NGame.Cli.PackAssetsCommand;



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

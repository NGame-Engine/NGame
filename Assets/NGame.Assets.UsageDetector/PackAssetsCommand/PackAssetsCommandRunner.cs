using Microsoft.Extensions.Logging;
using NGame.Cli.Abstractions;
using NGame.Cli.PackAssetsCommand.AssetFileReaders;
using NGame.Cli.PackAssetsCommand.CommandValidators;
using NGame.Cli.PackAssetsCommand.Specifications;
using NGame.Cli.PackAssetsCommand.Writers;

namespace NGame.Cli.PackAssetsCommand;



public class PackAssetsCommandRunner(
	ILogger<PackAssetsCommandRunner> logger,
	ICommandValidator commandValidator,
	IAssetListLineParser assetListLineParser,
	ISpecificationCreator specificationCreator,
	IAssetsWriter assetsWriter
)
	: ICommandRunner
{
	public void Run()
	{
		logger.LogInformation("Packing assets...");


		var validatedCommand = commandValidator.ValidateCommand();
		logger.LogInformation(
			"Input validated, project file {AssetFile}, target folder {Target}",
			validatedCommand.AssetList.Value,
			validatedCommand.TargetFolder.Value
		);


		var assetFileEntries =
			assetListLineParser.ReadAssetFileEntries(validatedCommand);
		logger.LogInformation("Found {Count} asset entries", assetFileEntries.Count);


		var assetPackSpecifications =
			specificationCreator
				.CreateSpecifications(validatedCommand, assetFileEntries);
		logger.LogInformation("{Count} pack specifications created", assetPackSpecifications.Count);


		assetsWriter.Write(validatedCommand, assetPackSpecifications);
		logger.LogInformation("Assets packs created");
	}
}

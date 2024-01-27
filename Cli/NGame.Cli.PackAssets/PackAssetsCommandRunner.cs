using Microsoft.Extensions.Logging;
using NGame.Cli.Abstractions;
using NGame.Cli.PackAssets.AssetFileReaders;
using NGame.Cli.PackAssets.CommandValidators;
using NGame.Cli.PackAssets.Specifications;
using NGame.Cli.PackAssets.Writers;

namespace NGame.Cli.PackAssets;



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

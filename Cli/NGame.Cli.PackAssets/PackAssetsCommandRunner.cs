using Microsoft.Extensions.Logging;
using NGame.Cli.Abstractions;
using NGame.Cli.PackAssets.AssetFileReaders;
using NGame.Cli.PackAssets.CommandValidators;
using NGame.Cli.PackAssets.Specifications;
using NGame.Cli.PackAssets.Writers;

namespace NGame.Cli.PackAssets;



public class PackAssetsCommandRunner : ICommandRunner
{
	private readonly ILogger<PackAssetsCommandRunner> _logger;
	private readonly ICommandValidator _commandValidator;
	private readonly IAssetListLineParser _assetListLineParser;
	private readonly ISpecificationCreator _specificationCreator;
	private readonly IAssetsWriter _assetsWriter;


	public PackAssetsCommandRunner(
		ILogger<PackAssetsCommandRunner> logger,
		ICommandValidator commandValidator,
		IAssetListLineParser assetListLineParser,
		ISpecificationCreator specificationCreator,
		IAssetsWriter assetsWriter
	)
	{
		_logger = logger;
		_commandValidator = commandValidator;
		_assetListLineParser = assetListLineParser;
		_specificationCreator = specificationCreator;
		_assetsWriter = assetsWriter;
	}


	public void Run()
	{
		_logger.LogInformation("Packing assets...");


		var validatedCommand = _commandValidator.ValidateCommand();
		_logger.LogInformation(
			"Input validated, project file {AssetFile}, target folder {Target}",
			validatedCommand.AssetList.Value,
			validatedCommand.TargetFolder.Value
		);


		var assetFileEntries =
			_assetListLineParser.ReadAssetFileEntries(validatedCommand);
		_logger.LogInformation("Found {Count} asset entries", assetFileEntries.Count);


		var assetPackSpecifications =
			_specificationCreator
				.CreateSpecifications(validatedCommand, assetFileEntries);
		_logger.LogInformation("{Count} pack specifications created", assetPackSpecifications.Count);


		_assetsWriter.Write(validatedCommand, assetPackSpecifications);
		_logger.LogInformation("Assets packs created");
	}
}

using Microsoft.Extensions.Logging;
using NGame.Cli.Abstractions;
using NGame.Cli.FindUsedAssetsCommand.AssetOverviews;
using NGame.Cli.FindUsedAssetsCommand.AssetUsages;
using NGame.Cli.FindUsedAssetsCommand.Commands;
using NGame.Cli.FindUsedAssetsCommand.FileWriters;

namespace NGame.Cli.FindUsedAssetsCommand;



internal class PackFindUsedAssetsCommandRunner(
	ILogger<PackFindUsedAssetsCommandRunner> logger,
	ICommandValidator commandValidator,
	IAssetOverviewCreator assetOverviewCreator,
	IAssetUsageFinder assetUsageFinder,
	IUsedAssetsFileWriter usedAssetsFileWriter
)
	: ICommandRunner
{
	public void Run()
	{
		logger.LogInformation("Packing assets...");

		var validatedCommand = commandValidator.ValidateCommand();
		logger.LogInformation(
			"Input validated, project file {AssetFile}, target folder {Target}",
			validatedCommand.SolutionDirectory.PathDisplay,
			validatedCommand.OutputDirectory.PathDisplay
		);

		var solutionDirectory = validatedCommand.SolutionDirectory;
		var assetOverview = assetOverviewCreator.Create(solutionDirectory);
		logger.LogInformation(
			"Assets overview created with {AssetEntriesCount} entries",
			assetOverview.AssetEntries.Count
		);

		var appSettingsPath = validatedCommand.AppSettings;
		var usedAssetOverview = assetUsageFinder.Create(appSettingsPath, assetOverview);
		logger.LogInformation(
			"Found {AssetEntriesCount} used asset entries",
			usedAssetOverview.UsedAssetEntries.Count
		);

		var outputPath = validatedCommand.OutputDirectory;
		usedAssetsFileWriter.WriteToFile(usedAssetOverview, appSettingsPath, outputPath);
		logger.LogInformation("Assets packs created");
	}
}

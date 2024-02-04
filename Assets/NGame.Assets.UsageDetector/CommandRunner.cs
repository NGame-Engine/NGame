using Microsoft.Extensions.Logging;
using NGame.Assets.UsageDetector.AssetOverviews;
using NGame.Assets.UsageDetector.AssetUsages;
using NGame.Assets.UsageDetector.Commands;
using NGame.Assets.UsageDetector.FileWriters;

namespace NGame.Assets.UsageDetector;



public interface ICommandRunner
{
	void Run();
}



internal class CommandRunner(
	ILogger<CommandRunner> logger,
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

using Microsoft.Extensions.Logging;
using NGame.Assets.Packer.AssetOverviews;
using NGame.Assets.Packer.AssetUsages;
using NGame.Assets.Packer.Commands;
using NGame.Assets.Packer.FileWriters;

namespace NGame.Assets.Packer;



public interface ICommandRunner
{
	void Run();
}



internal class CommandRunner(
	ILogger<CommandRunner> logger,
	ICommandValidator commandValidator,
	IAssetOverviewCreator assetOverviewCreator,
	IAssetUsageFinder assetUsageFinder,
	IAssetPackFileWriter assetPackFileWriter
)
	: ICommandRunner
{
	public void Run()
	{
		logger.LogInformation("Packing assets...");

		var validatedCommand = commandValidator.ValidateCommand();

		var assetOverview = assetOverviewCreator.Create( validatedCommand);
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

		var outputPath = validatedCommand.Output;
		assetPackFileWriter.WriteToFile(usedAssetOverview, outputPath);
		logger.LogInformation("Assets packs created");
	}
}

using Microsoft.Extensions.Logging;
using NGame.Assets.UsageFinder.AssetUsages;
using NGame.Assets.UsageFinder.Commands;

namespace NGame.Assets.UsageFinder.Setup;



public interface ICommandRunner
{
	AssetUsageOverview Run(TaskParameters taskParameters);
}



internal class CommandRunner(
	ILogger<CommandRunner> logger,
	IParameterValidator parameterValidator,
	IAssetUsageFinder assetUsageFinder
)
	: ICommandRunner
{
	public AssetUsageOverview Run(TaskParameters taskParameters)
	{
		logger.LogInformation("Packing assets...");

		var validatedCommand = parameterValidator.ValidateCommand(taskParameters);

		var assetListPaths = validatedCommand.AssetListPaths;
		var appSettingsPath = validatedCommand.AppSettingsPath;
		var usedAssetOverview = assetUsageFinder.Find(assetListPaths, appSettingsPath);

		logger.LogInformation(
			"Found {AssetEntriesCount} used asset entries",
			usedAssetOverview.UsedAssetEntries.Count
		);

		return usedAssetOverview;
	}
}

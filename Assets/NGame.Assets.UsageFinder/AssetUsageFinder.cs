using Microsoft.Extensions.Logging;
using NGame.Assets.UsageFinder.AssetOverviews;
using NGame.Assets.UsageFinder.AssetUsages;
using Singulink.IO;

namespace NGame.Assets.UsageFinder;



public interface IAssetUsageFinder
{
	AssetUsageOverview Find(IAbsoluteDirectoryPath assetListPaths, IEnumerable<IAbsoluteFilePath> appSettingsPath);
}



internal class AssetUsageFinder(
	ILogger<AssetUsageFinder> logger,
	IAssetOverviewCreator assetOverviewCreator,
	IAssetUsageOverviewFactory assetUsageOverviewFactory
)
	: IAssetUsageFinder
{
	public AssetUsageOverview Find(
		IAbsoluteDirectoryPath solutionDirectory,
		IEnumerable<IAbsoluteFilePath> appSettingsFiles
	)
	{
		var assetOverview = assetOverviewCreator.Create(solutionDirectory);
		logger.LogInformation(
			"Assets overview created with {AssetEntriesCount} entries",
			assetOverview.AssetEntries.Count
		);

		var usedAssetOverview = assetUsageOverviewFactory.Create(appSettingsFiles, assetOverview);
		logger.LogInformation(
			"Found {AssetEntriesCount} used asset entries",
			usedAssetOverview.UsedAssetEntries.Count
		);

		return usedAssetOverview;
	}
}

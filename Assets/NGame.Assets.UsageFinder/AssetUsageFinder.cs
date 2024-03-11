using Microsoft.Extensions.Logging;
using NGame.Assets.UsageFinder.AssetOverviews;
using NGame.Assets.UsageFinder.AssetUsages;
using Singulink.IO;

namespace NGame.Assets.UsageFinder;



public interface IAssetUsageFinder
{
	AssetUsageOverview Find(IEnumerable<IAbsoluteFilePath> assetListPaths, IAbsoluteFilePath appSettingsPath);
}



internal class AssetUsageFinder(
	ILogger<AssetUsageFinder> logger,
	IAssetOverviewCreator assetOverviewCreator,
	IAssetUsageOverviewFactory assetUsageOverviewFactory
)
	: IAssetUsageFinder
{
	public AssetUsageOverview Find(
		IEnumerable<IAbsoluteFilePath> assetListPaths,
		IAbsoluteFilePath appSettingsPath
	)
	{
		var assetOverview = assetOverviewCreator.Create(assetListPaths);
		logger.LogInformation(
			"Assets overview created with {AssetEntriesCount} entries",
			assetOverview.AssetEntries.Count
		);

		var usedAssetOverview = assetUsageOverviewFactory.Create(appSettingsPath, assetOverview);
		logger.LogInformation(
			"Found {AssetEntriesCount} used asset entries",
			usedAssetOverview.UsedAssetEntries.Count
		);

		return usedAssetOverview;
	}
}

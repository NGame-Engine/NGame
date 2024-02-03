using NGame.Cli.FindUsedAssetsCommand.AssetOverviews;

namespace NGame.Cli.FindUsedAssetsCommand.AssetUsages;



internal class AssetUsageOverview(IEnumerable<AssetEntry> usedAssetEntries)
{
	public List<AssetEntry> UsedAssetEntries { get; } = [.. usedAssetEntries];
}

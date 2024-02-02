using NGame.Cli.FindUsedAssets.AssetOverviews;

namespace NGame.Cli.FindUsedAssets.AssetUsages;



internal class AssetUsageOverview(IEnumerable<AssetEntry> usedAssetEntries)
{
	public List<AssetEntry> UsedAssetEntries { get; } = [.. usedAssetEntries];
}

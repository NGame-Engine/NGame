using NGame.Assets.UsageDetector.AssetOverviews;

namespace NGame.Assets.UsageDetector.AssetUsages;



internal class AssetUsageOverview(IEnumerable<AssetEntry> usedAssetEntries)
{
	public List<AssetEntry> UsedAssetEntries { get; } = [.. usedAssetEntries];
}

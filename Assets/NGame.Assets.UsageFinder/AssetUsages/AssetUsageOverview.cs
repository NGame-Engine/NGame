using NGame.Assets.UsageFinder.AssetOverviews;

namespace NGame.Assets.UsageFinder.AssetUsages;



public class AssetUsageOverview(IEnumerable<AssetEntry> usedAssetEntries)
{
	public List<AssetEntry> UsedAssetEntries { get; } = [.. usedAssetEntries];
}

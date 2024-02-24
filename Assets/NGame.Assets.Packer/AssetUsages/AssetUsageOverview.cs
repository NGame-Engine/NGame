using NGame.Assets.Packer.AssetOverviews;

namespace NGame.Assets.Packer.AssetUsages;



internal class AssetUsageOverview(IEnumerable<AssetEntry> usedAssetEntries)
{
	public List<AssetEntry> UsedAssetEntries { get; } = [.. usedAssetEntries];
}

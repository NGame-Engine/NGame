namespace NGame.Assets.UsageDetector.AssetOverviews;



internal class AssetOverview(
	IEnumerable<AssetEntry> assetEntries
)
{
	public List<AssetEntry> AssetEntries { get; } = [.. assetEntries];
}

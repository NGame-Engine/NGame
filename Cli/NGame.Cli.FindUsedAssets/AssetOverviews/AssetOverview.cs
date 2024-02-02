namespace NGame.Cli.FindUsedAssets.AssetOverviews;



internal class AssetOverview(
	IEnumerable<AssetEntry> assetEntries
)
{
	public List<AssetEntry> AssetEntries { get; } = [..assetEntries];
}

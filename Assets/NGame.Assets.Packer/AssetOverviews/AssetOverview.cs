using Singulink.IO;

namespace NGame.Assets.Packer.AssetOverviews;



public record PathInfo(IAbsoluteFilePath SourcePath, IRelativeFilePath TargetPath);



public record AssetEntry(
	Guid Id,
	PathInfo MainPathInfo,
	string PackageName,
	PathInfo? SatellitePathInfo
);



internal class AssetOverview(
	IEnumerable<AssetEntry> assetEntries
)
{
	public List<AssetEntry> AssetEntries { get; } = [.. assetEntries];
}

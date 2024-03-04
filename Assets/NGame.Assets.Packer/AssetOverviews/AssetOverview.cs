using Singulink.IO;

namespace NGame.Assets.Packer.AssetOverviews;



public class PathInfo(IAbsoluteFilePath sourcePath, IRelativeFilePath targetPath)
{
	public IAbsoluteFilePath SourcePath { get; init; } = sourcePath;


	public string GetNormalizedZipPath() =>
		targetPath
			.PathDisplay
			.Replace('\\', '/');
}



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

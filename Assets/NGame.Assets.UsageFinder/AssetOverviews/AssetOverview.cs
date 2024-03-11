using Singulink.IO;

namespace NGame.Assets.UsageFinder.AssetOverviews;



public class PathInfo(
	IAbsoluteDirectoryPath projectDirectory,
	IAbsoluteFilePath sourcePath,
	IRelativeFilePath targetPath
)
{
	public IAbsoluteDirectoryPath ProjectDirectory { get; init; } = projectDirectory;
	public IAbsoluteFilePath SourcePath { get; init; } = sourcePath;
	public IRelativeFilePath TargetPath { get; init; } = targetPath;


	public string GetNormalizedZipPath() =>
		TargetPath
			.PathDisplay
			.Replace('\\', '/');
}



public record AssetEntry(
	Guid Id,
	IAbsoluteDirectoryPath ProjectDirectory,
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

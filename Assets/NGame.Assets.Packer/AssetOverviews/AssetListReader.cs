using NGame.Assets.Common.Assets;
using Singulink.IO;

namespace NGame.Assets.Packer.AssetOverviews;



public record AssetFileInfo(
	PathInfo MainPathInfo,
	string PackageName,
	PathInfo? SatellitePathInfo
);



public record PathInfo(IAbsoluteFilePath SourcePath, IRelativeFilePath TargetPath);



public interface IAssetListReader
{
	IEnumerable<AssetFileInfo> ReadEntries(
		IEnumerable<IRelativeFilePath> assetFilePaths,
		IAbsoluteDirectoryPath unpackedAssetsDirectory
	);
}



public class AssetListReader : IAssetListReader
{
	private static readonly char[] PathSeparators =
	[
		Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar
	];


	public IEnumerable<AssetFileInfo> ReadEntries(
		IEnumerable<IRelativeFilePath> assetFilePaths,
		IAbsoluteDirectoryPath unpackedAssetsDirectory
	)
	{
		var allFilePaths =
			assetFilePaths
				.Select(x => x.PathDisplay.Replace('\\', '/'))
				.ToHashSet();

		return allFilePaths
			.Where(x => x.EndsWith(AssetConventions.AssetFileEnding))
			.Select(x => CreateAssetFileInfo(x, unpackedAssetsDirectory, allFilePaths));
	}


	private static AssetFileInfo CreateAssetFileInfo(
		string assetFilePath,
		IAbsoluteDirectoryPath unpackedAssetsDirectory,
		IReadOnlySet<string> allFilePaths
	)
	{
		var absoluteFilePath = unpackedAssetsDirectory.CombineFile(assetFilePath);

		var (packageName, filePathString) = SplitPath(assetFilePath);
		var relativeFilePath = FilePath.ParseRelative(filePathString);
		var pathInfo = new PathInfo(absoluteFilePath, relativeFilePath);

		var satellitePathInfo = GetSatelliteFile(
			assetFilePath, allFilePaths, unpackedAssetsDirectory
		);

		return new AssetFileInfo(pathInfo, packageName, satellitePathInfo);
	}


	private static (string PackageName, string FilePath) SplitPath(string path)
	{
		var lineParts = path.Split(PathSeparators, 2);
		var packageName = lineParts[0];
		var filePathString = lineParts[1];
		return (packageName, filePathString);
	}


	private static PathInfo? GetSatelliteFile(
		string assetFilePath,
		IReadOnlySet<string> allFilePaths,
		IAbsoluteDirectoryPath unpackedAssetsDirectory
	)
	{
		var satelliteFilePath =
			assetFilePath[..^AssetConventions.AssetFileEnding.Length];

		if (allFilePaths.Contains(satelliteFilePath) == false) return null;

		var absoluteFilePath = unpackedAssetsDirectory.CombineFile(satelliteFilePath);

		var lineParts = SplitPath(satelliteFilePath);
		var filePathString = lineParts.FilePath;
		var relativeFilePath = FilePath.ParseRelative(filePathString);

		return new PathInfo(absoluteFilePath, relativeFilePath);
	}
}

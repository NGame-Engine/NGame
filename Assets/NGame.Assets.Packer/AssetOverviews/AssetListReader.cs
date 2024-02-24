using NGame.Assets.Common.Assets;
using Singulink.IO;

namespace NGame.Assets.Packer.AssetOverviews;



public record AssetFileInfo(
	PathInfo MainPathInfo,
	string PackageName,
	PathInfo? SatellitePathInfo
);



public record PathInfo(IAbsoluteFilePath AbsolutePath, IRelativeFilePath RelativePath);



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
		var assetFileSet =
			assetFilePaths
				.Select(x => x.PathDisplay)
				.ToHashSet();

		return assetFileSet
			.Where(x => x.EndsWith(AssetConventions.AssetFileEnding))
			.Select(x => CreateAssetFileInfo(x, unpackedAssetsDirectory, assetFileSet));
	}


	private static AssetFileInfo CreateAssetFileInfo(
		string assetFilePath,
		IAbsoluteDirectoryPath unpackedAssetsDirectory,
		IReadOnlySet<string> allLinesSet
	)
	{
		var lineParts = SplitPath(assetFilePath);
		var packageName = lineParts.PackageName;
		var filePathString = lineParts.FilePath;
		var pathInfo = CreatePathInfo(filePathString, unpackedAssetsDirectory);


		var satellitePathInfo = GetSatelliteFile(
			assetFilePath, allLinesSet, unpackedAssetsDirectory
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


	private static PathInfo CreatePathInfo(
		string filePathString,
		IAbsoluteDirectoryPath unpackedAssetsDirectory
	)
	{
		var relativeFilePath = FilePath.ParseRelative(filePathString);
		var absoluteFilePath = unpackedAssetsDirectory.CombineFile(filePathString);
		return new PathInfo(absoluteFilePath, relativeFilePath);
	}


	private static PathInfo? GetSatelliteFile(
		string assetFilePath,
		IReadOnlySet<string> assetListLines,
		IAbsoluteDirectoryPath unpackedAssetsDirectory
	)
	{
		var satelliteFilePath =
			assetFilePath[..^AssetConventions.AssetFileEnding.Length];

		if (assetListLines.Contains(satelliteFilePath) == false) return null;

		var lineParts = SplitPath(satelliteFilePath);
		var filePathString = lineParts.FilePath;

		return CreatePathInfo(filePathString, unpackedAssetsDirectory);
	}
}

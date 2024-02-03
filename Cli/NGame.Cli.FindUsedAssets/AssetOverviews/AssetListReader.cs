using NGame.Assets;
using NGame.Tooling.Assets;
using Singulink.IO;

namespace NGame.Cli.FindUsedAssets.AssetOverviews;



public record AssetFileInfo(
	IAbsoluteFilePath FilePath,
	string PackageName,
	IAbsoluteFilePath? CompanionFile
);



public interface IAssetListReader
{
	IEnumerable<AssetFileInfo> ReadEntries(IEnumerable<string> lines);
}



public class AssetListReader : IAssetListReader
{
	public IEnumerable<AssetFileInfo> ReadEntries(IEnumerable<string> lines)
	{
		var allLinesSet = lines.ToHashSet();

		return
			allLinesSet
				.Where(x => x.EndsWith(AssetConventions.AssetFileEnding))
				.Select(x => CreateAssetFileInfo(x, allLinesSet));
	}


	private static AssetFileInfo CreateAssetFileInfo(
		string assetFileLine,
		ICollection<string> allLinesSet
	)
	{
		var lineParts = assetFileLine.Split(AssetConventions.PackSeparator);
		var packageName = lineParts[0];
		var filePathString = lineParts[1];
		var filePath = FilePath.ParseAbsolute(filePathString);

		var companionFilePath = GetCompanionFile(assetFileLine, allLinesSet);

		return new AssetFileInfo(filePath, packageName, companionFilePath);
	}


	private static IAbsoluteFilePath? GetCompanionFile(
		string assetFilePath,
		ICollection<string> assetListLines
	)
	{
		var companionFileLine = assetFilePath[..^AssetConventions.AssetFileEnding.Length];
		if (assetListLines.Contains(companionFileLine) == false) return null;

		var lineParts = companionFileLine.Split(AssetConventions.PackSeparator);
		var filePathString = lineParts[1];
		return FilePath.ParseAbsolute(filePathString);
	}
}

using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Assets.Common.Assets;
using NGame.Assets.Packer.Commands;
using Singulink.IO;

namespace NGame.Assets.Packer.AssetOverviews;



internal class JsonAsset : Asset;



internal interface IAssetOverviewCreator
{
	AssetOverview Create(ValidatedCommand validatedCommand);
}



internal class AssetOverviewCreator(
	IEnumerable<JsonConverter> jsonConverters
) : IAssetOverviewCreator
{
	private record AssetFileInfo(PathInfo MainPathInfo, PathInfo? SatellitePathInfo);


	public AssetOverview Create(ValidatedCommand validatedCommand)
	{
		var options = new JsonSerializerOptions();

		foreach (var jsonConverter in jsonConverters)
		{
			options.Converters.Add(jsonConverter);
		}

		var allFilePaths =
			validatedCommand
				.AssetListPaths
				.SelectMany(File.ReadAllLines)
				.ToHashSet();


		var assetEntries =
			allFilePaths
				.Where(x => x.EndsWith(AssetConventions.AssetFileEnding))
				.Select(x => CreateAssetFileInfo(x, allFilePaths))
				.Select(info => CreateAssetEntry(info, options));


		return new AssetOverview(assetEntries);
	}


	private static AssetFileInfo CreateAssetFileInfo(
		string assetFilePath,
		IReadOnlySet<string> allFilePaths
	)
	{
		var mainPathInfo = ParseFileLine(assetFilePath);

		var satelliteFilePath =
			assetFilePath[..^AssetConventions.AssetFileEnding.Length];

		var satellitePathInfo =
			allFilePaths.Contains(satelliteFilePath)
				? ParseFileLine(satelliteFilePath)
				: null;

		return new AssetFileInfo(mainPathInfo, satellitePathInfo);
	}


	private static PathInfo ParseFileLine(string fileLine)
	{
		var pathParts = fileLine.Split(AssetConventions.RelativePathSeparator, 2);

		var sourcePath =
			DirectoryPath
				.ParseAbsolute(pathParts[0])
				.CombineFile(pathParts[1]);

		var targetPath = FilePath.ParseRelative(pathParts[1]);

		return new PathInfo(sourcePath, targetPath);
	}


	private static AssetEntry CreateAssetEntry(
		AssetFileInfo assetFileInfo,
		JsonSerializerOptions options
	)
	{
		var mainPathInfo = assetFileInfo.MainPathInfo;
		var allText = File.ReadAllText(mainPathInfo.SourcePath.PathDisplay);
		var jsonAsset = JsonSerializer.Deserialize<JsonAsset>(allText, options)!;

		return new AssetEntry(
			jsonAsset.Id,
			mainPathInfo,
			jsonAsset.PackageName,
			assetFileInfo.SatellitePathInfo
		);
	}
}

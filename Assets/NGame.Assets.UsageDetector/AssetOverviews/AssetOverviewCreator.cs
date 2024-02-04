using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Tooling.Assets;
using Singulink.IO;

namespace NGame.Assets.UsageDetector.AssetOverviews;



internal interface IAssetOverviewCreator
{
	AssetOverview Create(IAbsoluteDirectoryPath solutionDirectory);
}



internal class AssetOverviewCreator(
	IAssetListReader assetListReader,
	IEnumerable<JsonConverter> jsonConverters
) : IAssetOverviewCreator
{
	public AssetOverview Create(IAbsoluteDirectoryPath solutionDirectory)
	{
		var options = new JsonSerializerOptions();

		foreach (var jsonConverter in jsonConverters)
		{
			options.Converters.Add(jsonConverter);
		}

		return new AssetOverview(
			Directory
				.EnumerateFiles(
					solutionDirectory.PathDisplay,
					$"*{AssetConventions.ListFileName}",
					SearchOption.AllDirectories
				)
				.Select(File.ReadAllLines)
				.SelectMany(assetListReader.ReadEntries)
				.Select(info => CreateAssetEntry(info, options))
		);
	}


	private AssetEntry CreateAssetEntry(
		AssetFileInfo assetFileInfo,
		JsonSerializerOptions options
	)
	{
		var filePath = assetFileInfo.FilePath;
		var allText = File.ReadAllText(filePath.PathDisplay);
		var jsonAsset = JsonSerializer.Deserialize<JsonAsset>(allText, options)!;

		return new AssetEntry(
			jsonAsset.Id.Id,
			filePath,
			assetFileInfo.PackageName,
			assetFileInfo.CompanionFile
		);
	}
}

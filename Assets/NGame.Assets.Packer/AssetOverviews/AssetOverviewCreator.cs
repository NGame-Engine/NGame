using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Assets.Packer.Commands;
using Singulink.IO;

namespace NGame.Assets.Packer.AssetOverviews;



internal class JsonAsset : Asset;



internal interface IAssetOverviewCreator
{
	AssetOverview Create(ValidatedCommand validatedCommand);
}



internal class AssetOverviewCreator(
	IAssetListReader assetListReader,
	IEnumerable<JsonConverter> jsonConverters
) : IAssetOverviewCreator
{
	public AssetOverview Create(ValidatedCommand validatedCommand)
	{
		var unpackedAssetsDirectory = validatedCommand.UnpackedAssetsDirectory;

		var assetFilePaths =
			unpackedAssetsDirectory
				.GetRelativeChildFiles(
					new SearchOptions
					{
						Recursive = true
					}
				);


		var options = new JsonSerializerOptions();

		foreach (var jsonConverter in jsonConverters)
		{
			options.Converters.Add(jsonConverter);
		}

		var assetEntries =
			assetListReader
				.ReadEntries(assetFilePaths, unpackedAssetsDirectory)
				.Select(info => CreateAssetEntry(info, options));


		return new AssetOverview(assetEntries);
	}


	private AssetEntry CreateAssetEntry(
		AssetFileInfo assetFileInfo,
		JsonSerializerOptions options
	)
	{
		var mainPathInfo = assetFileInfo.MainPathInfo;
		var allText = File.ReadAllText(mainPathInfo.AbsolutePath.PathDisplay);
		var jsonAsset = JsonSerializer.Deserialize<JsonAsset>(allText, options)!;

		return new AssetEntry(
			jsonAsset.Id,
			mainPathInfo,
			assetFileInfo.PackageName,
			assetFileInfo.SatellitePathInfo
		);
	}
}

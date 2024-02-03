using System.Text.Json;
using NGame.Assets;
using NGame.Tooling.Assets;
using Singulink.IO;

namespace NGame.Cli.FindUsedAssets.AssetOverviews;



internal interface IAssetOverviewCreator
{
	AssetOverview Create(IAbsoluteDirectoryPath solutionDirectory);
}



internal class AssetOverviewCreator(
	IAssetListReader assetListReader
) : IAssetOverviewCreator
{
	public AssetOverview Create(IAbsoluteDirectoryPath solutionDirectory) =>
		new(
			Directory
				.EnumerateFiles(
					solutionDirectory.PathExport,
					$"*ngame/{AssetConventions.ListFileName}",
					SearchOption.AllDirectories
				)
				.Select(File.ReadAllLines)
				.SelectMany(assetListReader.ReadEntries)
				.Select(CreateAssetEntry)
		);


	private AssetEntry CreateAssetEntry(AssetFileInfo assetFileInfo)
	{
		var filePath = assetFileInfo.FilePath;
		var allText = File.ReadAllText(filePath.PathExport);
		var jsonAsset = JsonSerializer.Deserialize<JsonAsset>(allText)!;

		return new AssetEntry(
			jsonAsset.Id.Id,
			filePath,
			assetFileInfo.PackageName,
			assetFileInfo.CompanionFile
		);
	}
}

using Microsoft.Extensions.Logging;
using NGame.Assets.Packer.AssetUsages;
using Singulink.IO;

namespace NGame.Assets.Packer.FileWriters;



internal interface IAssetPackFileWriter
{
	public void WriteToFile(
		AssetUsageOverview assetUsageOverview,
		IAbsoluteDirectoryPath outputPath
	);
}



internal class AssetPackFileWriter(
	ILogger<AssetPackFileWriter> logger,
	IAssetPackFactory assetPackFactory,
	ITableOfContentsWriter tableOfContentsWriter
) : IAssetPackFileWriter
{
	public void WriteToFile(
		AssetUsageOverview assetUsageOverview,
		IAbsoluteDirectoryPath outputPath
	)
	{
		var assetFileSpecifications = assetUsageOverview
			.UsedAssetEntries
			.ToList();


		outputPath.Create();
		var createdPackNames = assetFileSpecifications
			.GroupBy(x => x.PackageName)
			.Select(x =>
				assetPackFactory.Create(x.Key, x, outputPath)
			);

		var packNamesString = string.Join(", ", createdPackNames);
		logger.LogInformation("Created asset packs {AssetPacks}", packNamesString);


		tableOfContentsWriter.Write(assetFileSpecifications, outputPath);
	}
}

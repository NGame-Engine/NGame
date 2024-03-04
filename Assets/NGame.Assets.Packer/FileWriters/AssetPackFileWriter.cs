using Microsoft.Extensions.Logging;
using NGame.Assets.Packer.AssetUsages;
using NGame.Assets.Packer.Commands;
using Singulink.IO;

namespace NGame.Assets.Packer.FileWriters;



internal interface IAssetPackFileWriter
{
	public void WriteToFile(
		AssetUsageOverview assetUsageOverview,
		IAbsoluteDirectoryPath outputPath,
		ValidatedCommand validatedCommand
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
		IAbsoluteDirectoryPath outputPath,
		ValidatedCommand validatedCommand
	)
	{
		var assetFileSpecifications = assetUsageOverview
			.UsedAssetEntries
			.ToList();


		validatedCommand.Output.Create();
		var createdPackNames = assetFileSpecifications
			.GroupBy(x => x.PackageName)
			.Select(x =>
				assetPackFactory.Create(x.Key, x, outputPath, validatedCommand)
			);

		var packNamesString = string.Join(", ", createdPackNames);
		logger.LogInformation("Created asset packs {AssetPacks}", packNamesString);


		tableOfContentsWriter.Write(assetFileSpecifications, outputPath);
	}
}
